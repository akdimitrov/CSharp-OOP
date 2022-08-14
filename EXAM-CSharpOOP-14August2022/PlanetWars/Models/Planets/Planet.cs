using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories;
using PlanetWars.Repositories.Contracts;
using PlanetWars.Utilities.Messages;
using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.Weapons;

namespace PlanetWars.Models.Planets
{
    public class Planet : IPlanet
    {
        private readonly IRepository<IMilitaryUnit> units;
        private readonly IRepository<IWeapon> weapons;
        private string name;
        private double budget;

        public Planet(string name, double budget)
        {
            units = new UnitRepository();
            weapons = new WeaponRepository();
            Name = name;
            Budget = budget;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPlanetName);
                }

                name = value;
            }
        }

        public double Budget
        {
            get => budget;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidBudgetAmount);
                }

                budget = value;
            }
        }

        public double MilitaryPower => CalcMilitaryPower();

        public IReadOnlyCollection<IMilitaryUnit> Army => units.Models;

        public IReadOnlyCollection<IWeapon> Weapons => weapons.Models;

        public void AddUnit(IMilitaryUnit unit)
            => units.AddItem(unit);

        public void AddWeapon(IWeapon weapon) 
            => weapons.AddItem(weapon);

        public string PlanetInfo()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Planet: {Name}");
            sb.AppendLine($"--Budget: {Budget} billion QUID");
            sb.AppendLine($"--Forces: {(Army.Any() ? string.Join(", ", Army.Select(x => x.GetType().Name)) : "No units")}");
            sb.AppendLine($"--Combat equipment: {(Weapons.Any() ? string.Join(", ", Weapons.Select(x => x.GetType().Name)) : "No weapons")}");
            sb.AppendLine($"--Military Power: {MilitaryPower}");

            return sb.ToString().TrimEnd();
        }

        public void Profit(double amount)
            => Budget += amount;

        public void Spend(double amount)
        {
            if (Budget < amount)
            {
                throw new InvalidOperationException(ExceptionMessages.UnsufficientBudget);
            }

            Budget -= amount;
        }

        public void TrainArmy()
        {
            foreach (var force in Army)
            {
                force.IncreaseEndurance();
            }
        }

        private double CalcMilitaryPower()
        {
            double totalAmount = Army.Sum(x => x.EnduranceLevel) + Weapons.Sum(x => x.DestructionLevel);

            if (Army.Any(x => x.GetType().Name == nameof(AnonymousImpactUnit)))
            {
                totalAmount *= 1.3;
            }

            if (Weapons.Any(x => x.GetType().Name == nameof(NuclearWeapon)))
            {
                totalAmount *= 1.45;
            }

            return Math.Round(totalAmount, 3);
        }
    }
}
