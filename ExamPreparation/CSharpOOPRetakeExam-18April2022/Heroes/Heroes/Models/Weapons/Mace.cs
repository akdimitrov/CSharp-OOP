using System;

namespace Heroes.Models.Weapons
{
    public class Mace : Weapon
    {
        private const int Damage = 25;

        public Mace(string name, int durability) : base(name, durability) { }

        public override int DoDamage()
        {
            int damage = Durability > 0 ? Damage : 0;
            Durability = Math.Max(Durability - 1, 0);
            return damage;
        }
    }
}
