using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Repositories.Contracts;

namespace PlanetWars.Repositories
{
    public class PlanetRepository : IRepository<IPlanet>
    {
        private readonly List<IPlanet> planets;

        public PlanetRepository()
        {
            planets = new List<IPlanet>();
        }

        public IReadOnlyCollection<IPlanet> Models => planets.AsReadOnly();

        public void AddItem(IPlanet model)
            => planets.Add(model);

        public IPlanet FindByName(string name)
            => Models.FirstOrDefault(x => x.Name == name);

        public bool RemoveItem(string name)
            => planets.Remove(FindByName(name));
    }
}
