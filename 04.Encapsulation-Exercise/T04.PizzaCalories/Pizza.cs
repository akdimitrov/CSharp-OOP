using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace T04.PizzaCalories
{
    public class Pizza
    {
        private string name;
        private readonly List<Topping> toppings;

        public Pizza(string name, Dough dough)
        {
            Name = name;
            Dough = dough;
            toppings = new List<Topping>();
        }

        public string Name
        {
            get => name;

            private set
            {
                if (string.IsNullOrWhiteSpace(value) ||
                    value.Length < 1 || value.Length > 15)
                {
                    throw new ArgumentException("Pizza name should be between 1 and 15 symbols.");
                }

                name = value;
            }
        }

        public Dough Dough { get; set; }

        public int NumberOfToppings => toppings.Count;

        public double Calories => Dough.Calories + toppings.Sum(x => x.Calories);

        public void AddTopping(Topping topping)
        {
            if (toppings.Count == 10)
            {
                throw new Exception("Number of toppings should be in range [0..10].");
            }

            toppings.Add(topping);
        }
    }
}
