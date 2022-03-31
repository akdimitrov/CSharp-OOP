using System;

namespace T04.PizzaCalories
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                string pizzaName = Console.ReadLine().Split()[1];
                string[] doughInfo = Console.ReadLine().Split();
                string flourType = doughInfo[1];
                string bakingTechnique = doughInfo[2];
                int weight = int.Parse(doughInfo[3]);

                Pizza pizza = new Pizza(pizzaName, new Dough(flourType, bakingTechnique, weight));
                string topping = Console.ReadLine();
                while (topping != "END")
                {
                    string[] toppingInfo = topping.Split();
                    string toppingType = toppingInfo[1];
                    int toppingWeight = int.Parse(toppingInfo[2]);

                    pizza.AddTopping(new Topping(toppingType, toppingWeight));
                    topping = Console.ReadLine();
                }

                Console.WriteLine($"{pizza.Name} - {pizza.Calories:f2} Calories.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
