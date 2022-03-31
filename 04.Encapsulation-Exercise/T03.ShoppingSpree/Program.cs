using System;
using System.Collections.Generic;
using System.Linq;

namespace T03.ShoppingSpree
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<Person> people = new List<Person>();
            List<Product> products = new List<Product>();
            string[] peopleInfo = Console.ReadLine()
                .Split(new char[] { ';', '=' }, StringSplitOptions.RemoveEmptyEntries);
            string[] productsInfo = Console.ReadLine()
                .Split(new char[] { ';', '=' }, StringSplitOptions.RemoveEmptyEntries);

            try
            {
                for (int i = 0; i < peopleInfo.Length - 1; i += 2)
                {
                    string name = peopleInfo[i];
                    decimal money = decimal.Parse(peopleInfo[i + 1]);
                    people.Add(new Person(name, money));
                }

                for (int i = 0; i < productsInfo.Length - 1; i += 2)
                {
                    string name = productsInfo[i];
                    decimal cost = decimal.Parse(productsInfo[i + 1]);
                    products.Add(new Product(name, cost));
                }

                string purchase = Console.ReadLine();
                while (purchase != "END")
                {
                    string[] purchaseInfo = purchase.Split();
                    string personName = purchaseInfo[0];
                    string productName = purchaseInfo[1];

                    people.First(x => x.Name == personName)
                        .BuyProduct(products.First(x => x.Name == productName));

                    purchase = Console.ReadLine();
                }

                foreach (var person in people)
                {
                    string productInfo = !person.Products.Any() ? "Nothing bought"
                        : string.Join(", ", person.Products.Select(x => x.Name));

                    Console.WriteLine($"{person.Name} - {productInfo}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
