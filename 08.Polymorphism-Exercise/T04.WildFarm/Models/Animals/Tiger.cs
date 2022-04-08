using T04.WildFarm.Contracts;
using T04.WildFarm.Exceptions;
using T04.WildFarm.Models.Foods;

namespace T04.WildFarm.Models.Animals
{
    public class Tiger : Feline
    {
        private const double Modifier = 1;

        public Tiger(string name, double weight, string livingRegion, string breed)
            : base(name, weight, livingRegion, breed) { }

        public override void Eat(IFood food)
        {
            if (food is Meat)
            {
                BaseEat(Modifier, food.Quantity);
            }
            else
            {
                InvalidOperations.ThrowInvalidFoodException(GetType().Name, food.GetType().Name);
            }
        }

        public override string ProduceSound() => "ROAR!!!";
    }
}
