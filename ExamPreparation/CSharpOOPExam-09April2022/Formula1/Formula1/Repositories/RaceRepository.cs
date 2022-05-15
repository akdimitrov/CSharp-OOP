using System.Collections.Generic;
using System.Linq;
using Formula1.Models.Contracts;
using Formula1.Repositories.Contracts;

namespace Formula1.Repositories
{
    public class RaceRepository : IRepository<IRace>
    {
        private readonly List<IRace> races;

        public RaceRepository()
        {
            races = new List<IRace>();
        }

        public IReadOnlyCollection<IRace> Models => races.AsReadOnly();

        public void Add(IRace race)
        {
            races.Add(race);
        }

        public bool Remove(IRace race)
        {
            return races.Remove(race);
        }

        public IRace FindByName(string raceName)
        {
            return races.FirstOrDefault(x => x.RaceName == raceName);
        }
    }
}
