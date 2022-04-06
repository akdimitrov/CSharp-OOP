using System;
using System.Collections.Generic;
using System.Text;
using T07.MilitaryElite.Contracts;

namespace T07.MilitaryElite.Models
{
    public class Commando : SpecialisedSoldier, ICommando
    {
        private readonly List<IMission> missions;

        public Commando(string id, string firstName, string lastName, decimal salary, string corps) : base(id, firstName, lastName, salary, corps)
        {
            missions = new List<IMission>();
        }

        public IReadOnlyCollection<IMission> Missions => missions.AsReadOnly();

        public void AddMissions(string[] missionsInfo)
        {
            for (int i = 0; i < missionsInfo.Length; i += 2)
            {
                string codeName = missionsInfo[i];
                string state = missionsInfo[i + 1];
                if (state == "inProgress" || state == "Finished")
                {
                    missions.Add(new Mission(codeName, state));
                }
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            sb.AppendLine("Missions:");
            missions.ForEach(x => sb.AppendLine($"  {x}"));
            return sb.ToString().TrimEnd();
        }
    }
}
