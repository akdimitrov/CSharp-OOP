using System;
using System.Collections.Generic;
using T04.WildFarm.Models.Foods;

namespace T04.WildFarm.Models.Animals
{
    public class Tiger : Feline
    {
        private const double TigerWeightMultiplier = 1;

        public Tiger(string name, double weight, string livingRegion, string breed)
            : base(name, weight, livingRegion, breed) { }

        protected override IReadOnlyCollection<Type> PreferredFoods => new List<Type> { typeof(Meat) }.AsReadOnly();

        protected override double WeightMultiplier => TigerWeightMultiplier;

        public override string ProduceSound() => "ROAR!!!";
    }
}
