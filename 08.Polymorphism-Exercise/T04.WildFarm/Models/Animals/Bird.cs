using T04.WildFarm.Models.Contracts;

namespace T04.WildFarm.Models.Animals
{
    public abstract class Bird : Animal, IBird
    {
        protected Bird(string name, double weight, double wingSize)
            : base(name, weight)
        {
            WingSize = wingSize;
        }

        public double WingSize { get; }

        public override string ToString()
        {
            return base.ToString() + $"{WingSize}, {Weight}, {FoodEaten}]";
        }
    }
}
