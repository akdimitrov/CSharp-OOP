using System;
using System.Collections.Generic;
using T04.WildFarm.Models.Foods;

namespace T04.WildFarm.Models.Animals
{
    public class Mouse : Mammal
    {
        private const double MouseWeightMultiplier = 0.10;

        public Mouse(string name, double weight, string livingRegion)
            : base(name, weight, livingRegion) { }

        protected override IReadOnlyCollection<Type> PreferredFoods => new List<Type> { typeof(Vegetable), typeof(Fruit) }.AsReadOnly();

        protected override double WeightMultiplier => MouseWeightMultiplier;

        public override string ProduceSound() => "Squeak";

        public override string ToString()
        {
            return base.ToString() + $"{Weight}, {LivingRegion}, {FoodEaten}]";
        }
    }
}
