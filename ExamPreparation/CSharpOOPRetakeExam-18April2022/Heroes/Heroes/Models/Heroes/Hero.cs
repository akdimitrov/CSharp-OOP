using System;
using Heroes.Models.Contracts;
using Heroes.Utilities;

namespace Heroes.Models.Heroes
{
    public abstract class Hero : IHero
    {
        private string name;
        private int health;
        private int armour;
        private IWeapon weapon;

        protected Hero(string name, int health, int armour)
        {
            Name = name;
            Health = health;
            Armour = armour;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidHeroName);
                }

                name = value;
            }
        }

        public int Health
        {
            get => health;
            private set
            {
                if (value < 0)
                {
                    health = 0;
                    throw new ArgumentException(ExceptionMessages.InvalidHeroHealth);
                }

                health = value;
            }
        }
        public int Armour
        {
            get => armour;
            private set
            {
                if (value < 0)
                {
                    armour = 0;
                    throw new ArgumentException(ExceptionMessages.InvalidHeroArmour);
                }

                armour = value;
            }
        }

        public IWeapon Weapon
        {
            get => weapon;
            private set
            {
                if (value == null)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidHeroWeapon);
                }

                weapon = value;
            }
        }

        public bool IsAlive => Health > 0;


        public void AddWeapon(IWeapon weapon)
        {
            Weapon = weapon;
        }

        public void TakeDamage(int points)
        {
            var diff = points - Armour;
            if (diff > 0)
            {
                Armour = 0;
                Health = Math.Max(Health - diff, 0);
            }
            else
            {
                Armour -= points;
            }
        }
    }
}
