using T04.WildFarm.Contracts;
using T04.WildFarm.Exceptions;
using T04.WildFarm.Models.Foods;

namespace T04.WildFarm.Models.Animals
{
    public class Cat : Feline
    {
        private const double Modifier = 0.30;

        public Cat(string name, double weight, string livingRegion, string breed)
            : base(name, weight, livingRegion, breed) { }

        public override void Eat(IFood food)
        {
            if (food is Vegetable || food is Meat)
            {
                BaseEat(Modifier, food.Quantity);
            }
            else
            {
                InvalidOperations.ThrowInvalidFoodException(GetType().Name, food.GetType().Name);
            }
        }

        public override string ProduceSound() => "Meow";
    }
}
