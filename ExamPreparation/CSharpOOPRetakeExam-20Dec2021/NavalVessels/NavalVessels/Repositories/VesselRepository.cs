using System.Collections.Generic;
using System.Linq;
using NavalVessels.Models.Contracts;
using NavalVessels.Repositories.Contracts;

namespace NavalVessels.Repositories
{
    public class VesselRepository : IRepository<IVessel>
    {
        private readonly List<IVessel> vessels;

        public VesselRepository()
        {
            vessels = new List<IVessel>();
        }

        public IReadOnlyCollection<IVessel> Models => vessels.AsReadOnly();

        public void Add(IVessel vessel)
        {
            vessels.Add(vessel);
        }

        public IVessel FindByName(string name)
        {
            return vessels.FirstOrDefault(x => x.Name == name);
        }

        public bool Remove(IVessel vessel)
        {
            return vessels.Remove(vessel);
        }
    }
}
