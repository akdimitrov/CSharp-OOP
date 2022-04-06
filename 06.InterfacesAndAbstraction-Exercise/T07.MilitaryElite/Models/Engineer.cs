using System;
using System.Collections.Generic;
using System.Text;
using T07.MilitaryElite.Contracts;

namespace T07.MilitaryElite.Models
{
    public class Engineer : SpecialisedSoldier, IEngineer
    {
        private readonly List<IRepair> repairs;

        public Engineer(string id, string firstName, string lastName, decimal salary, string corps) : base(id, firstName, lastName, salary, corps)
        {
            repairs = new List<IRepair>();
        }

        public IReadOnlyCollection<IRepair> Repairs => repairs.AsReadOnly();

        public void AddRepairs(string[] repairsInfo)
        {
            for (int i = 0; i < repairsInfo.Length; i += 2)
            {
                repairs.Add(new Repair(repairsInfo[i], int.Parse(repairsInfo[i + 1])));
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            sb.AppendLine("Repairs:");
            repairs.ForEach(x => sb.AppendLine($"  {x}"));
            return sb.ToString().TrimEnd();
        }
    }
}
