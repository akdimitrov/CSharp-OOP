using System;

using WarCroft.Constants;
using WarCroft.Entities.Inventory;
using WarCroft.Entities.Items;

namespace WarCroft.Entities.Characters.Contracts
{
    public abstract class Character
    {
        private string name;
        private double health;

        public Character(string name, double health, double armor, double abilityPoints, Bag bag)
        {
            BaseHealth = health;
            BaseArmor = armor;
            Name = name;
            Health = health;
            Armor = armor;
            AbilityPoints = abilityPoints;
            Bag = bag;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.CharacterNameInvalid);
                }

                name = value;
            }
        }

        public double BaseHealth { get; }

        public double Health
        {
            get => health;
            set
            {
                EnsureAlive();
                health = value;
                if (health <= 0)
                {
                    IsAlive = false;
                    health = 0;
                }
                else if (health > BaseHealth)
                {
                    health = BaseHealth;
                }
            }
        }

        public double BaseArmor { get; }

        public double Armor { get; private set; }

        public double AbilityPoints { get; set; }

        public Bag Bag { get; set; }

        public bool IsAlive { get; set; } = true;

        public void TakeDamage(double hitPoints)
        {
            EnsureAlive();
            Armor -= hitPoints;
            if (Armor < 0)
            {
                Health -= Math.Abs(Armor);
                Armor = 0;
            }
        }

        public void UseItem(Item item)
        {
            item.AffectCharacter(this);
        }

        protected void EnsureAlive()
        {
            if (!IsAlive)
            {
                throw new InvalidOperationException(ExceptionMessages.AffectedCharacterDead);
            }
        }
    }
}