using System;
using System.Collections.Generic;
using T04.WildFarm.Models.Foods;

namespace T04.WildFarm.Models.Animals
{
    public class Dog : Mammal
    {
        private const double DogWeightMultiplier = 0.40;

        public Dog(string name, double weight, string livingRegion)
            : base(name, weight, livingRegion) { }

        protected override IReadOnlyCollection<Type> PreferredFoods => new List<Type> { typeof(Meat) }.AsReadOnly();

        protected override double WeightMultiplier => DogWeightMultiplier;

        public override string ProduceSound() => "Woof!";

        public override string ToString()
        {
            return base.ToString() + $"{Weight}, {LivingRegion}, {FoodEaten}]";
        }
    }
}
