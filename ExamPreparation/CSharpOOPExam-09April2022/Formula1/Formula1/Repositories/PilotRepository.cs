using System.Collections.Generic;
using System.Linq;
using Formula1.Models.Contracts;
using Formula1.Repositories.Contracts;

namespace Formula1.Repositories
{
    public class PilotRepository : IRepository<IPilot>
    {
        private readonly List<IPilot> pilots;

        public PilotRepository()
        {
            pilots = new List<IPilot>();
        }

        public IReadOnlyCollection<IPilot> Models => pilots.AsReadOnly();

        public void Add(IPilot pilot)
        {
            pilots.Add(pilot);
        }

        public bool Remove(IPilot pilot)
        {
            return pilots.Remove(pilot);
        }

        public IPilot FindByName(string fullName)
        {
            return pilots.FirstOrDefault(x => x.FullName == fullName);
        }
    }
}
