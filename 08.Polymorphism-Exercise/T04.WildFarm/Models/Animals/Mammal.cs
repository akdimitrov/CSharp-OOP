using T04.WildFarm.Contracts;

namespace T04.WildFarm.Models.Animals
{
    public abstract class Mammal : Animal, IMammal
    {
        protected Mammal(string name, double weight, string livingRegion)
            : base(name, weight)
        {
            LivingRegion = livingRegion;
        }

        public string LivingRegion { get; }
    }
}
