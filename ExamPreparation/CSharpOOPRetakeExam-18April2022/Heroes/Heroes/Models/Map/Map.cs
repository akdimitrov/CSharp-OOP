using System.Collections.Generic;
using System.Linq;
using Heroes.Models.Contracts;
using Heroes.Models.Heroes;
using Heroes.Utilities;

namespace Heroes.Models.Map
{
    public class Map : IMap
    {
        public string Fight(ICollection<IHero> players)
        {
            var knights = players
                .Where(x => x.GetType().Name == nameof(Knight) &&
                x.IsAlive &&
                x.Weapon != null)
                .ToList();
            var barbarians = players
                .Where(x => x.GetType().Name == nameof(Barbarian) &&
                x.IsAlive &&
                x.Weapon != null)
                .ToList();

            string result;
            while (true)
            {
                foreach (var knight in knights.Where(x => x.IsAlive))
                {
                    foreach (var barbarian in barbarians.Where(x => x.IsAlive))
                    {
                        barbarian.TakeDamage(knight.Weapon.DoDamage());
                    }
                }

                if (barbarians.All(x => !x.IsAlive))
                {
                    result = string.Format(OutputMessages.KnightsWon, knights.Count(x => !x.IsAlive));
                    break;
                }

                foreach (var barbarian in barbarians.Where(x => x.IsAlive))
                {
                    foreach (var knight in knights.Where(x => x.IsAlive))
                    {
                        knight.TakeDamage(barbarian.Weapon.DoDamage());
                    }
                }

                if (knights.All(x => !x.IsAlive))
                {
                    result = string.Format(OutputMessages.BarberiansWon, barbarians.Count(x => !x.IsAlive));
                    break;
                }
            }

            return result;
        }
    }
}
