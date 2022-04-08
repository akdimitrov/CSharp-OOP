using T04.WildFarm.Contracts;

namespace T04.WildFarm.Models.Animals
{
    public abstract class Animal : IAnimal
    {
        public Animal(string name, double weight)
        {
            Name = name;
            Weight = weight;
        }

        public string Name { get; }

        public double Weight { get; protected set; }

        public int FoodEaten { get; protected set; }

        public abstract void Eat(IFood food);

        public abstract string ProduceSound();

        protected void BaseEat(double modifier, int quantity)
        {
            Weight += modifier * quantity;
            FoodEaten += quantity;
        }
    }
}
