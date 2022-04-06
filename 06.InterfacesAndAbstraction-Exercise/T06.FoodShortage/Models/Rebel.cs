using System;
using System.Collections.Generic;
using System.Text;
using T06.FoodShortage.Contracts;

namespace T06.FoodShortage.Models
{
    public class Rebel : IBuyer
    {
        public Rebel(string name, int age, string group)
        {
            Name = name;
            Age = age;
            Group = group;
        }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Group { get; set; }

        public int Food { get; set; }

        public void BuyFood()
        {
            Food += 5;
        }
    }
}
