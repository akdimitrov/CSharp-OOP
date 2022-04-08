using System;
using System.Collections.Generic;
using System.Linq;
using T03.Raiding.Contractcs;
using T03.Raiding.Models.Factories;

namespace T03.Raiding.Core
{
    public class Engine
    {
        public void Run()
        {
            List<IHero> heroes = new List<IHero>();
            int n = int.Parse(Console.ReadLine());
            while (heroes.Count < n)
            {
                string name = Console.ReadLine();
                string type = Console.ReadLine();

                IHero hero = HeroFactory.Create(type, name);
                if (hero is null)
                {
                    Console.WriteLine("Invalid hero!");
                    continue;
                }

                heroes.Add(hero);
            }

            int bossPower = int.Parse(Console.ReadLine());
            int heroesPower = heroes.Sum(x => x.Power);
            heroes.ForEach(x => Console.WriteLine(x.CastAbility()));
            Console.WriteLine(heroesPower >= bossPower ? "Victory!" : "Defeat...");
        }
    }
}
