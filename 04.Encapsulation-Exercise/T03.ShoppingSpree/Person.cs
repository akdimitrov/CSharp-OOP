using System;
using System.Collections.Generic;
using System.Text;

namespace T03.ShoppingSpree
{
    public class Person
    {
        private readonly List<Product> products;
        private string name;
        private decimal money;

        public Person(string name, decimal money)
        {
            Name = name;
            Money = money;
            products = new List<Product>();
        }

        public IReadOnlyCollection<Product> Products => products.AsReadOnly();

        public string Name
        {
            get => name;

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be empty");
                }

                name = value;
            }
        }

        public decimal Money
        {
            get => money;

            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Money cannot be negative");
                }

                money = value;
            }
        }

        public void BuyProduct(Product product)
        {
            if (money - product.Cost < 0)
            {
                Console.WriteLine($"{name} can't afford {product.Name}");
                return;
            }

            money -= product.Cost;
            products.Add(product);
            Console.WriteLine($"{name} bought {product.Name}");
        }
    }
}
