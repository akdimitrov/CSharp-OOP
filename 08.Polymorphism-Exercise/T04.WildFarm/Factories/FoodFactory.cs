using T04.WildFarm.Exceptions;
using T04.WildFarm.Factories.Contratcts;
using T04.WildFarm.Models.Contracts;
using T04.WildFarm.Models.Foods;

namespace T04.WildFarm.Factories
{
    public class FoodFactory : IFoodFactory
    {
        public IFood Create(string[] foodInfo)
        {
            string foodType = foodInfo[0];
            int foodQuantity = int.Parse(foodInfo[1]);

            IFood food = null;
            if (foodType == "Vegetable")
            {
                food = new Vegetable(foodQuantity);
            }
            else if (foodType == "Fruit")
            {
                food = new Fruit(foodQuantity);
            }
            else if (foodType == "Meat")
            {
                food = new Meat(foodQuantity);
            }
            else if (foodType == "Seeds")
            {
                food = new Seeds(foodQuantity);
            }
            else
            {
                throw new InvalidFactoryTypeException();
            }

            return food;
        }
    }
}
