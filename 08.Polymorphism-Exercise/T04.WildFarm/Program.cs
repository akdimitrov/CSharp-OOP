using T04.WildFarm.Core;
using T04.WildFarm.Factories;
using T04.WildFarm.Factories.Contratcts;
using T04.WildFarm.IO;
using T04.WildFarm.IO.Contracts;

namespace T04.WildFarm
{
    public class Program
    {
        static void Main(string[] args)
        {
            IAnimalFactory animalFactory = new AnimalFactory();
            IFoodFactory foodFactory = new FoodFactory();

            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();

            IEngine engine = new Engine(animalFactory, foodFactory, reader, writer);
            engine.Run();
        }
    }
}
