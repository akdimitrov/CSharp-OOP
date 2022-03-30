using System;
using System.Collections.Generic;

namespace Animals
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            List<Animal> animals = new List<Animal>();
            string type;
            while ((type = Console.ReadLine()) != "Beast!")
            {
                try
                {
                    string[] animalInfo = Console.ReadLine().Split();
                    string name = animalInfo[0];
                    int age = int.Parse(animalInfo[1]);
                    Animal animal = null;
                    switch (type)
                    {
                        case "Dog": animal = new Dog(name, age, animalInfo[2]); break;
                        case "Cat": animal = new Cat(name, age, animalInfo[2]); break;
                        case "Frog": animal = new Frog(name, age, animalInfo[2]); break;
                        case "Kitten": animal = new Kitten(name, age); break;
                        case "Tomcat": animal = new Tomcat(name, age); break;
                        default: continue;
                    }

                    animals.Add(animal);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            animals.ForEach(Console.WriteLine);
        }
    }
}
