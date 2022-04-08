namespace T03.Raiding.Models
{
    public abstract class BaseFighter : BaseHero
    {
        public BaseFighter(string name, int power) : base(name, power) { }

        public override string CastAbility()
        {
            return $"{base.CastAbility()} hit for {Power} damage";
        }
    }
}
