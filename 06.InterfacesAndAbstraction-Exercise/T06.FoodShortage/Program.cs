using System;
using System.Collections.Generic;
using System.Linq;
using T06.FoodShortage.Contracts;
using T06.FoodShortage.Models;

namespace T06.FoodShortage
{
    public class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, IBuyer> buyers = new Dictionary<string, IBuyer>();
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                string[] buyerInfo = Console.ReadLine().Split();
                string buyerName = buyerInfo[0];
                int age = int.Parse(buyerInfo[1]);

                if (buyerInfo.Length == 4)
                {
                    buyers[buyerName] = new Citizen(buyerName, age, buyerInfo[2], buyerInfo[3]);
                }
                else if (buyerInfo.Length == 3)
                {
                    buyers[buyerName] = new Rebel(buyerName, age, buyerInfo[2]);
                }
            }

            string name = Console.ReadLine();
            while (name != "End")
            {
                if (buyers.ContainsKey(name))
                {
                    buyers[name].BuyFood();
                }

                name = Console.ReadLine();
            }

            Console.WriteLine(buyers.Sum(x => x.Value.Food));
        }
    }
}
