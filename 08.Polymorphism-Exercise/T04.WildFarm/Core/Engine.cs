using System;
using System.Collections.Generic;
using T04.WildFarm.Exceptions;
using T04.WildFarm.Factories.Contratcts;
using T04.WildFarm.IO.Contracts;
using T04.WildFarm.Models.Contracts;

namespace T04.WildFarm.Core
{
    public class Engine : IEngine
    {
        private readonly IAnimalFactory animalFactory;
        private readonly IFoodFactory foodFactory;
        private readonly IReader reader;
        private readonly IWriter writer;

        private readonly ICollection<IAnimal> animals;

        public Engine(IAnimalFactory animalFactory, IFoodFactory foodFactory, IReader reader, IWriter writer)
        {
            this.animalFactory = animalFactory;
            this.foodFactory = foodFactory;
            this.reader = reader;
            this.writer = writer;
            animals = new List<IAnimal>();
        }

        public void Run()
        {
            string input = reader.ReadLine();
            while (input != "End")
            {
                try
                {
                    string[] animalInfo = input.Split();
                    string[] foodInfo = reader.ReadLine().Split();

                    IAnimal animal = animalFactory.Create(animalInfo);
                    IFood food = foodFactory.Create(foodInfo);
                    writer.WriteLine(animal.ProduceSound());
                    animals.Add(animal);
                    animal.Eat(food);
                }
                catch (InvalidFactoryTypeException ifte)
                {
                    writer.WriteLine(ifte.Message);
                }
                catch (FoodNotPrefferedException fnpe)
                {
                    writer.WriteLine(fnpe.Message);
                }
                catch (InvalidOperationException ioe)
                {
                    writer.WriteLine(ioe.Message);
                }

                input = reader.ReadLine();
            }

            foreach (var animal in animals)
            {
                writer.WriteLine(animal.ToString());
            }
        }
    }
}
