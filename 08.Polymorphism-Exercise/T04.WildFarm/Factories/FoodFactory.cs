using T04.WildFarm.Contracts;
using T04.WildFarm.Models.Foods;

namespace T04.WildFarm.Models.Factories
{
    public static class FoodFactory
    {
        public static IFood Create(string[] foodInfo)
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

            return food;
        }
    }
}
