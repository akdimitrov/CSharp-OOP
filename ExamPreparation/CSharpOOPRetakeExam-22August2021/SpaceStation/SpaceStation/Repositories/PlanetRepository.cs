namespace SpaceStation.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Models.Planets.Contracts;

    public class PlanetRepository : IRepository<IPlanet>
    {
        private readonly List<IPlanet> models;

        public PlanetRepository()
        {
            models = new List<IPlanet>();
        }

        public IReadOnlyCollection<IPlanet> Models => models.AsReadOnly();

        public void Add(IPlanet model)
            => models.Add(model);

        public IPlanet FindByName(string name)
            => models.FirstOrDefault(x => x.Name == name);

        public bool Remove(IPlanet model)
            => models.Remove(model);
    }
}
