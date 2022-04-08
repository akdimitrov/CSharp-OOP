namespace T04.WildFarm.Contracts
{
    public interface IAnimal
    {
        string Name { get; }

        double Weight { get; }

        int FoodEaten { get; }

        void Eat(IFood food);

        string ProduceSound();
    }
}
