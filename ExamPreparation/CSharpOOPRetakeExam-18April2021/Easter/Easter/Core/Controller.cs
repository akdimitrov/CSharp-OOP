using System;
using System.Linq;
using System.Text;
using Easter.Core.Contracts;
using Easter.Models.Bunnies;
using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes;
using Easter.Models.Eggs;
using Easter.Models.Eggs.Contracts;
using Easter.Models.Workshops;
using Easter.Repositories;
using Easter.Repositories.Contracts;
using Easter.Utilities.Messages;

namespace Easter.Core
{
    public class Controller : IController
    {
        private readonly IRepository<IBunny> bunnies;
        private readonly IRepository<IEgg> eggs;

        public Controller()
        {
            bunnies = new BunnyRepository();
            eggs = new EggRepository();
        }

        public string AddBunny(string bunnyType, string bunnyName)
        {
            IBunny bunny;
            switch (bunnyType)
            {
                case "HappyBunny": bunny = new HappyBunny(bunnyName); break;
                case "SleepyBunny": bunny = new SleepyBunny(bunnyName); break;
                default: throw new InvalidOperationException(ExceptionMessages.InvalidBunnyType);
            }

            bunnies.Add(bunny);
            return string.Format(OutputMessages.BunnyAdded, bunnyType, bunnyName);
        }

        public string AddDyeToBunny(string bunnyName, int power)
        {
            var bunny = bunnies.FindByName(bunnyName);
            if (bunny == null)
            {
                throw new InvalidOperationException(ExceptionMessages.InexistentBunny);
            }

            bunny.AddDye(new Dye(power));
            return string.Format(OutputMessages.DyeAdded, power, bunnyName);
        }

        public string AddEgg(string eggName, int energyRequired)
        {
            var egg = new Egg(eggName, energyRequired);
            eggs.Add(egg);
            return string.Format(OutputMessages.EggAdded, eggName);
        }

        public string ColorEgg(string eggName)
        {
            var readyBunnies = bunnies.Models.Where(x => x.Energy >= 50).OrderByDescending(x => x.Energy).ToList();
            if (!readyBunnies.Any())
            {
                throw new InvalidOperationException(ExceptionMessages.BunniesNotReady);
            }

            var workshop = new Workshop();
            var egg = eggs.FindByName(eggName);
            foreach (var bunny in readyBunnies)
            {
                workshop.Color(egg, bunny);

                if (bunny.Energy == 0)
                {
                    bunnies.Remove(bunny);
                }

                if (egg.IsDone())
                {
                    break;
                }
            }

            return egg.IsDone() ?
                string.Format(OutputMessages.EggIsDone, eggName) :
                string.Format(OutputMessages.EggIsNotDone, eggName);
        }

        public string Report()
        {
            StringBuilder report = new StringBuilder();
            report.AppendLine($"{eggs.Models.Count(x => x.IsDone())} eggs are done!");
            report.AppendLine("Bunnies info:");
            foreach (var bunny in bunnies.Models)
            {
                report.AppendLine($"Name: {bunny.Name}");
                report.AppendLine($"Energy: {bunny.Energy}");
                report.AppendLine($"Dyes: {bunny.Dyes.Count} not finished");
            }

            return report.ToString().TrimEnd();
        }
    }
}
