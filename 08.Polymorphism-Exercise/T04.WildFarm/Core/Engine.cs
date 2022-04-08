using System;
using System.Collections.Generic;
using T04.WildFarm.Contracts;
using T04.WildFarm.Models.Factories;

namespace T04.WildFarm.Core
{
    public class Engine
    {
        public void Run()
        {
            List<IAnimal> animals = new List<IAnimal>();
            string input = Console.ReadLine();
            while (input != "End")
            {
                try
                {
                    string[] animalInfo = input.Split();
                    string[] foodInfo = Console.ReadLine().Split();

                    IAnimal animal = AnimalFactory.Create(animalInfo);
                    IFood food = FoodFactory.Create(foodInfo);
                    Console.WriteLine(animal.ProduceSound());
                    animals.Add(animal);
                    animal.Eat(food);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                input = Console.ReadLine();
            }

            animals.ForEach(x => Console.WriteLine(x));
        }
    }
}
