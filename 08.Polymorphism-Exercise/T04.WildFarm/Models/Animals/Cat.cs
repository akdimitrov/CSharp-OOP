using System;
using System.Collections.Generic;
using T04.WildFarm.Models.Foods;

namespace T04.WildFarm.Models.Animals
{
    public class Cat : Feline
    {
        private const double CatWeightMultiplier = 0.30;

        public Cat(string name, double weight, string livingRegion, string breed)
            : base(name, weight, livingRegion, breed) { }

        protected override IReadOnlyCollection<Type> PreferredFoods => new List<Type> { typeof(Vegetable), typeof(Meat) }.AsReadOnly();

        protected override double WeightMultiplier => CatWeightMultiplier;

        public override string ProduceSound() => "Meow";
    }
}
