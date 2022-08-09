namespace T04.WildFarm.Models.Contracts
{
    public interface IMammal : IAnimal
    {
        string LivingRegion { get; }
    }
}
