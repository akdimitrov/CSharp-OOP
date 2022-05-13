using System;

namespace Heroes.Models.Weapons
{
    public class Claymore : Weapon
    {
        private const int Damage = 20;

        public Claymore(string name, int durability) : base(name, durability) { }

        public override int DoDamage()
        {
            int damage = Durability > 0 ? Damage : 0;
            Durability = Math.Max(Durability - 1, 0);
            return damage;
        }
    }
}
