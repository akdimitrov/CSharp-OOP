using T04.WildFarm.Contracts;
using T04.WildFarm.Models.Animals;

namespace T04.WildFarm.Models.Factories
{
    public static class AnimalFactory
    {
        public static IAnimal Create(string[] animalInfo)
        {
            string animalType = animalInfo[0];
            string name = animalInfo[1];
            double weight = double.Parse(animalInfo[2]);

            IAnimal animal = null;
            if (animalType == "Cat")
            {
                animal = new Cat(name, weight, animalInfo[3], animalInfo[4]);
            }
            else if (animalType == "Tiger")
            {
                animal = new Tiger(name, weight, animalInfo[3], animalInfo[4]);
            }
            else if (animalType == "Mouse")
            {
                animal = new Mouse(name, weight, animalInfo[3]);
            }
            else if (animalType == "Dog")
            {
                animal = new Dog(name, weight, animalInfo[3]);
            }
            else if (animalType == "Owl")
            {
                animal = new Owl(name, weight, double.Parse(animalInfo[3]));
            }
            else if (animalType == "Hen")
            {
                animal = new Hen(name, weight, double.Parse(animalInfo[3]));
            }

            return animal;
        }
    }
}
