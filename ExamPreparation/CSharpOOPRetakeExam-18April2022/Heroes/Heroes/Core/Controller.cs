using System;
using System.Linq;
using System.Text;
using Heroes.Core.Contracts;
using Heroes.Models.Contracts;
using Heroes.Models.Heroes;
using Heroes.Models.Map;
using Heroes.Models.Weapons;
using Heroes.Repositories;
using Heroes.Repositories.Contracts;
using Heroes.Utilities;

namespace Heroes.Core
{
    public class Controller : IController
    {
        private readonly IRepository<IHero> heroes;
        private readonly IRepository<IWeapon> weapons;
        private readonly IMap map;

        public Controller()
        {
            heroes = new HeroRepository();
            weapons = new WeaponRepository();
            map = new Map();
        }

        public string AddWeaponToHero(string weaponName, string heroName)
        {
            var hero = heroes.FindByName(heroName);
            var weapon = weapons.FindByName(weaponName);

            if (hero == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.HeroDoesNotExist, heroName));
            }
            else if (weapon == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.WeaponDoesNotExist, weaponName));
            }
            else if (hero.Weapon != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.HeroAlreadyHasAWeapon, heroName));
            }

            weapons.Remove(weapon);
            hero.AddWeapon(weapon);
            return string.Format(OutputMessages.HeroCanParticipateInBattle, heroName, weapon.GetType().Name.ToLower());
        }

        public string CreateHero(string type, string name, int health, int armour)
        {
            IHero hero = heroes.FindByName(name);
            if (hero != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.HeroAlreadyExist, name));
            }

            string message;
            if (type == nameof(Knight))
            {
                hero = new Knight(name, health, armour);
                message = string.Format(OutputMessages.KnightAdded, name);
            }
            else if (type == nameof(Barbarian))
            {
                hero = new Barbarian(name, health, armour);
                message = string.Format(OutputMessages.BarbarianAdded, name);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidHeroType);
            }

            heroes.Add(hero);
            return message;
        }

        public string CreateWeapon(string type, string name, int durability)
        {
            IWeapon weapon = weapons.FindByName(name);
            if (weapon != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.WeaponAlreadyExist, name));
            }

            if (type == nameof(Mace))
            {
                weapon = new Mace(name, durability);
            }
            else if (type == nameof(Claymore))
            {
                weapon = new Claymore(name, durability);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidWeaponType);
            }

            weapons.Add(weapon);
            return string.Format(OutputMessages.WeaponAdded, type.ToLower(), name);
        }

        public string StartBattle()
        {
            return map.Fight(heroes.Models.ToList());
        }

        public string HeroReport()
        {
            StringBuilder report = new StringBuilder();
            foreach (var hero in heroes.Models
                .OrderBy(x => x.GetType().Name)
                .ThenByDescending(x => x.Health)
                .ThenBy(x => x.Name))
            {
                report.AppendLine($"{hero.GetType().Name}: {hero.Name}");
                report.AppendLine($"--Health: {hero.Health}");
                report.AppendLine($"--Armour: {hero.Armour}");
                string weapon = hero.Weapon != null ? hero.Weapon.Name : "Unarmed";
                report.AppendLine($"--Weapon: {weapon}");
            }

            return report.ToString().TrimEnd();
        }
    }
}
