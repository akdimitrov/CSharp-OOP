using T04.WildFarm.Contracts;

namespace T04.WildFarm.Models.Animals
{
    public class Hen : Bird
    {
        private const double Modifier = 0.35;

        public Hen(string name, double weight, double wingSize)
            : base(name, weight, wingSize) { }

        public override void Eat(IFood food)
        {
            BaseEat(Modifier, food.Quantity);
        }

        public override string ProduceSound() => "Cluck";
    }
}
