namespace T03.Raiding.Models
{
    public abstract class BaseHealer : BaseHero
    {
        public BaseHealer(string name, int power) : base(name, power) { }

        public override string CastAbility()
        {
            return $"{base.CastAbility()} healed for {Power}";
        }
    }
}
