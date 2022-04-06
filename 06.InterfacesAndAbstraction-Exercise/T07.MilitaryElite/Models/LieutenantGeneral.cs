using System.Collections.Generic;
using System.Text;
using T07.MilitaryElite.Contracts;

namespace T07.MilitaryElite.Models
{
    public class LieutenantGeneral : Private, ILieutenantGeneral
    {
        private List<ISoldier> privates;

        public LieutenantGeneral(string id, string firstName, string lastName, decimal salary) : base(id, firstName, lastName, salary)
        {
            privates = new List<ISoldier>();
        }

        public IReadOnlyCollection<ISoldier> Privates => privates.AsReadOnly();

        public void AddPrivates(Dictionary<string, ISoldier> soldiers, string[] ids)
        {
            foreach (var id in ids)
            {
                privates.Add(soldiers[id]);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            sb.AppendLine("Privates:");
            privates.ForEach(x => sb.AppendLine($"  {x}"));
            return sb.ToString().TrimEnd();
        }
    }
}
