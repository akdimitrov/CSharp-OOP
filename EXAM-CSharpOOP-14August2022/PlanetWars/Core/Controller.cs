using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlanetWars.Core.Contracts;
using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories;
using PlanetWars.Repositories.Contracts;
using PlanetWars.Utilities.Messages;

namespace PlanetWars.Core
{
    public class Controller : IController
    {
        private readonly IRepository<IPlanet> planets;

        public Controller()
        {
            planets = new PlanetRepository();
        }

        public string AddUnit(string unitTypeName, string planetName)
        {
            var planet = planets.FindByName(planetName);

            if (planet == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }

            IMilitaryUnit unit = unitTypeName switch
            {
                nameof(SpaceForces) => new SpaceForces(),
                nameof(StormTroopers) => new StormTroopers(),
                nameof(AnonymousImpactUnit) => new AnonymousImpactUnit(),
                _ => throw new InvalidOperationException(string.Format(ExceptionMessages.ItemNotAvailable, unitTypeName))
            };

            if (planet.Army.Any(x => x.GetType().Name == unit.GetType().Name))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnitAlreadyAdded, unitTypeName, planetName));
            }

            planet.Spend(unit.Cost);
            planet.AddUnit(unit);

            return string.Format(OutputMessages.UnitAdded, unitTypeName, planetName);
        }

        public string AddWeapon(string planetName, string weaponTypeName, int destructionLevel)
        {
            var planet = planets.FindByName(planetName);

            if (planet == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }

            if (planet.Weapons.Any(x => x.GetType().Name == weaponTypeName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.WeaponAlreadyAdded, weaponTypeName, planetName));
            }

            IWeapon weapon = weaponTypeName switch
            {
                nameof(BioChemicalWeapon) => new BioChemicalWeapon(destructionLevel),
                nameof(NuclearWeapon) => new NuclearWeapon(destructionLevel),
                nameof(SpaceMissiles) => new SpaceMissiles(destructionLevel),
                _ => throw new InvalidOperationException(string.Format(ExceptionMessages.ItemNotAvailable, weaponTypeName))
            };

            

            planet.Spend(weapon.Price);
            planet.AddWeapon(weapon);

            return string.Format(OutputMessages.WeaponAdded, planetName, weaponTypeName);
        }

        public string CreatePlanet(string name, double budget)
        {
            if (planets.FindByName(name) != null)
            {
                return string.Format(OutputMessages.ExistingPlanet, name);
            }

            var planet = new Planet(name, budget);
            planets.AddItem(planet);

            return string.Format(OutputMessages.NewPlanet, name);
        }

        public string SpaceCombat(string planetOne, string planetTwo)
        {
            var firstPlanet = planets.FindByName(planetOne);
            var secondPlanet = planets.FindByName(planetTwo);

            IPlanet winningPlanet = null;
            IPlanet losingPlanet = null;

            if (firstPlanet.MilitaryPower > secondPlanet.MilitaryPower)
            {
                winningPlanet = firstPlanet;
                losingPlanet = secondPlanet;
            }
            else if (secondPlanet.MilitaryPower > firstPlanet.MilitaryPower)
            {
                winningPlanet = secondPlanet;
                losingPlanet = firstPlanet;
            }
            else
            {
                bool firstPlanetHasNuclrearWeapon = firstPlanet.Weapons.Any(x => x.GetType().Name == nameof(NuclearWeapon));
                bool secondPlanetHasNuclrearWeapon = secondPlanet.Weapons.Any(x => x.GetType().Name == nameof(NuclearWeapon));

                if (firstPlanetHasNuclrearWeapon == secondPlanetHasNuclrearWeapon)
                {
                    firstPlanet.Spend(firstPlanet.Budget / 2);
                    secondPlanet.Spend(secondPlanet.Budget / 2);
                    return OutputMessages.NoWinner;
                }
                else if (firstPlanetHasNuclrearWeapon)
                {
                    winningPlanet = firstPlanet;
                    losingPlanet = secondPlanet;
                }
                else if (secondPlanetHasNuclrearWeapon)
                {
                    winningPlanet = secondPlanet;
                    losingPlanet = firstPlanet;
                }
            }

            winningPlanet.Spend(winningPlanet.Budget / 2);
            winningPlanet.Profit(losingPlanet.Budget / 2);
            winningPlanet.Profit(losingPlanet.Weapons.Sum(x => x.Price));
            winningPlanet.Profit(losingPlanet.Army.Sum(x => x.Cost));

            planets.RemoveItem(losingPlanet.Name);

            return string.Format(OutputMessages.WinnigTheWar, winningPlanet.Name, losingPlanet.Name);
        }

        public string SpecializeForces(string planetName)
        {
            var planet = planets.FindByName(planetName);

            if (planet == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }

            if (!planet.Army.Any())
            {
                throw new InvalidOperationException(ExceptionMessages.NoUnitsFound);
            }

            planet.Spend(1.25);
            planet.TrainArmy();

            return string.Format(OutputMessages.ForcesUpgraded, planetName);
        }

        public string ForcesReport()
        {
            var sb = new StringBuilder();
            sb.AppendLine("***UNIVERSE PLANET MILITARY REPORT***");
            foreach (var planet in planets.Models.OrderByDescending(x => x.MilitaryPower).ThenBy(x => x.Name))
            {
                sb.AppendLine(planet.PlanetInfo());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
