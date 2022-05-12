using System.Linq;
using Easter.Models.Bunnies.Contracts;
using Easter.Models.Eggs.Contracts;
using Easter.Models.Workshops.Contracts;

namespace Easter.Models.Workshops
{
    public class Workshop : IWorkshop
    {
        public void Color(IEgg egg, IBunny bunny)
        {
            var dye = bunny.Dyes.FirstOrDefault();
            while (dye != null && !egg.IsDone() && bunny.Energy > 0)
            {
                if (dye.IsFinished())
                {
                    bunny.Dyes.Remove(dye);
                    dye = bunny.Dyes.FirstOrDefault();
                }
                else
                {
                    bunny.Work();
                    dye.Use();
                    egg.GetColored();
                }
            }

            if (dye != null && dye.IsFinished())
            {
                bunny.Dyes.Remove(dye);
            }
        }
    }
}
