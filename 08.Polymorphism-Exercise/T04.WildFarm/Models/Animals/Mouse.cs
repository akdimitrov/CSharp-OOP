using T04.WildFarm.Contracts;
using T04.WildFarm.Exceptions;
using T04.WildFarm.Models.Foods;

namespace T04.WildFarm.Models.Animals
{
    public class Mouse : Mammal
    {
        private const double Modifier = 0.10;

        public Mouse(string name, double weight, string livingRegion)
            : base(name, weight, livingRegion) { }

        public override void Eat(IFood food)
        {
            if (food is Vegetable || food is Fruit)
            {
                BaseEat(Modifier, food.Quantity);
            }
            else
            {
                InvalidOperations.ThrowInvalidFoodException(GetType().Name, food.GetType().Name);
            }
        }

        public override string ProduceSound() => "Squeak";

        public override string ToString()
        {
            return $"{GetType().Name} [{Name}, {Weight}, {LivingRegion}, {FoodEaten}]";
        }
    }
}
