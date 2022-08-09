using T04.WildFarm.Models.Contracts;

namespace T04.WildFarm.Models.Animals
{
    public abstract class Feline : Mammal, IFeline
    {
        protected Feline(string name, double weight, string livingRegion, string breed)
            : base(name, weight, livingRegion)
        {
            Breed = breed;
        }

        public string Breed { get; }

        public override string ToString()
        {
            return base.ToString() + $"{Breed}, {Weight}, {LivingRegion}, {FoodEaten}]";
        }
    }
}
