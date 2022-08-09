using T04.WildFarm.Models.Contracts;

namespace T04.WildFarm.Factories.Contratcts
{
    public interface IAnimalFactory
    {
        IAnimal Create(string[] animalInfo);
    }
}
