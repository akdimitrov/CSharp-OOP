namespace AxeAndDummy
{
    public class Hero : IHero
    {
        private readonly string name;
        private int experience;
        private readonly IWeapon weapon;

        public Hero(string name, IWeapon weapon)
        {
            this.name = name;
            this.weapon = weapon;
        }

        public string Name => this.name;

        public int Experience => this.experience;

        public IWeapon Weapon => this.weapon;

        public void Attack(ITarget target)
        {
            this.weapon.Attack(target);
            if (target.IsDead())
            {
                this.experience += target.GiveExperience();
            }
        }
    }
}
