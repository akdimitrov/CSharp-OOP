namespace SpaceStation.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Contracts;
    using Models.Astronauts.Contracts;
    using Models.Mission;
    using Models.Mission.Contracts;
    using Models.Planets;
    using Models.Planets.Contracts;
    using Repositories;
    using Repositories.Contracts;
    using SpaceStation.Models.Astronauts;
    using Utilities.Messages;

    public class Controller : IController
    {
        private readonly IRepository<IAstronaut> astronautRepository;
        private readonly IRepository<IPlanet> planetRepository;
        private readonly IMission mission;
        private readonly HashSet<string> exploredPlantes;

        public Controller()
        {
            astronautRepository = new AstronautRepository();
            planetRepository = new PlanetRepository();
            mission = new Mission();
            exploredPlantes = new HashSet<string>();
        }

        public string AddAstronaut(string type, string astronautName)
        {
            IAstronaut astronaut = null;
            if (type == "Biologist")
            {
                astronaut = new Biologist(astronautName);
            }
            else if (type == "Geodesist")
            {
                astronaut = new Geodesist(astronautName);
            }
            else if (type == "Meteorologist")
            {
                astronaut = new Meteorologist(astronautName);
            }

            if (astronaut == null)
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAstronautType);
            }

            astronautRepository.Add(astronaut);

            return string.Format(OutputMessages.AstronautAdded, type, astronautName);
        }

        public string AddPlanet(string planetName, params string[] items)
        {
            IPlanet planet = new Planet(planetName);
            foreach (var item in items)
            {
                planet.Items.Add(item);
            }

            planetRepository.Add(planet);

            return string.Format(OutputMessages.PlanetAdded, planetName);
        }

        public string ExplorePlanet(string planetName)
        {
            IPlanet planet = planetRepository.FindByName(planetName);

            var suitableAustranauts = astronautRepository.Models.Where(x => x.Oxygen > 60).ToList();
            if (suitableAustranauts.Count == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAstronautCount);
            }

            int beforeMissionDeaths = astronautRepository.Models.Count(x => !x.CanBreath);

            mission.Explore(planet, suitableAustranauts);
            exploredPlantes.Add(planetName);

            int afterMissionDeaths = astronautRepository.Models.Count(x => !x.CanBreath);

            return string.Format(OutputMessages.PlanetExplored, planetName, afterMissionDeaths - beforeMissionDeaths);
        }

        public string RetireAstronaut(string astronautName)
        {
            var astrounaut = astronautRepository.FindByName(astronautName);
            if (astrounaut == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidRetiredAstronaut, astronautName));
            }

            astronautRepository.Remove(astrounaut);

            return string.Format(OutputMessages.AstronautRetired, astronautName);
        }

        public string Report()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{exploredPlantes.Count} planets were explored!");
            sb.AppendLine("Astronauts info:");
            foreach (var austronaut in astronautRepository.Models)
            {
                var items = austronaut.Bag.Items.Any() ? string.Join(", ", austronaut.Bag.Items) : "none";

                sb.AppendLine($"Name: {austronaut.Name}");
                sb.AppendLine($"Oxygen: {austronaut.Oxygen}");
                sb.AppendLine($"Bag items: {items}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
