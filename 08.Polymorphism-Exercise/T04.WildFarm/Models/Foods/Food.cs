using T04.WildFarm.Contracts;

namespace T04.WildFarm.Models.Foods
{
    public abstract class Food : IFood
    {
        protected Food(int quantity)
        {
            Quantity = quantity;
        }

        public int Quantity { get; }
    }
}
