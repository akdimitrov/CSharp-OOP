using T04.WildFarm.Contracts;
using T04.WildFarm.Exceptions;
using T04.WildFarm.Models.Foods;

namespace T04.WildFarm.Models.Animals
{
    public class Owl : Bird
    {
        private const double Modifier = 0.25;

        public Owl(string name, double weight, double wingSize)
            : base(name, weight, wingSize) { }

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

        public override string ProduceSound() => "Hoot Hoot";
    }
}
