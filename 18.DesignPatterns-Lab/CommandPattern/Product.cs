using System;

namespace CommandPattern
{
    public class Product
    {
        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public void IncreasePrice(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Invalid amount");
            }

            Price += amount;
            Console.WriteLine($"The price for the {Name} has been increased by {amount}$.");
        }

        public void DecreasePrice(decimal amount)
        {
            if (amount > Price || amount <= 0)
            {
                throw new ArgumentException("Invalid amount");
            }

            Price -= amount;
            Console.WriteLine($"The price for the {Name} has been decreased by {amount}$.");
        }

        public override string ToString()
        => $"Current price for the {Name} product is {Price}$.";

    }
}
