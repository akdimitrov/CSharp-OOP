using T04.WildFarm.Models.Contracts;

namespace T04.WildFarm.Factories.Contratcts
{
    public interface IFoodFactory
    {
        IFood Create(string[] foodInfo);
    }
}
