using System;
using System.Collections.Generic;
using System.Text;
using T06.FoodShortage.Contracts;

namespace T06.FoodShortage.Models
{
    public class Pet : IBirthable
    {
        public Pet(string name, string birthdate)
        {
            Name = name;
            Birthdate = birthdate;
        }

        public string Name { get; set; }

        public string Birthdate { get; set; }
    }
}
