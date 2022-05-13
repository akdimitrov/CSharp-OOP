using System;
using Heroes.Models.Contracts;
using Heroes.Utilities;

namespace Heroes.Models.Weapons
{
    public abstract class Weapon : IWeapon
    {
        private string name;
        private int durability;

        protected Weapon(string name, int durability)
        {
            Name = name;
            Durability = durability;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidWeponName);
                }

                name = value;
            }
        }

        public int Durability
        {
            get => durability;
            protected set
            {
                if (value < 0)
                {
                    durability = 0;
                    throw new ArgumentException(ExceptionMessages.InvalidDurability);
                }

                durability = value;
            }
        }

        public abstract int DoDamage();
    }
}
