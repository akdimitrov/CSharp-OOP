using T03.Raiding.Contractcs;

namespace T03.Raiding.Models
{
    public abstract class BaseHero : IHero
    {
        protected BaseHero(string name, int power)
        {
            Name = name;
            Power = power;
        }

        public string Name { get; set; }

        public int Power { get; set; }

        public virtual string CastAbility()
        {
            return $"{GetType().Name} - {Name}";
        }
    }
}
