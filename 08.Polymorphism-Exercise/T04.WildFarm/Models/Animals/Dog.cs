using T04.WildFarm.Contracts;
using T04.WildFarm.Exceptions;
using T04.WildFarm.Models.Foods;

namespace T04.WildFarm.Models.Animals
{
    public class Dog : Mammal
    {
        private const double Modifier = 0.40;

        public Dog(string name, double weight, string livingRegion)
            : base(name, weight, livingRegion) { }

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

        public override string ProduceSound() => "Woof!";

        public override string ToString()
        {
            return $"{GetType().Name} [{Name}, {Weight}, {LivingRegion}, {FoodEaten}]";
        }
    }
}
