using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Repositories.Contracts;

namespace PlanetWars.Repositories
{
    public class UnitRepository : IRepository<IMilitaryUnit>
    {
        private readonly List<IMilitaryUnit> units;

        public UnitRepository()
        {
            units = new List<IMilitaryUnit>();
        }

        public IReadOnlyCollection<IMilitaryUnit> Models => units.AsReadOnly();

        public void AddItem(IMilitaryUnit model)
            => units.Add(model);

        public IMilitaryUnit FindByName(string name)
            => Models.FirstOrDefault(x => x.GetType().Name == name);

        public bool RemoveItem(string name)
             => units.Remove(FindByName(name));
    }
}
