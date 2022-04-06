using System;
using System.Collections.Generic;
using System.Text;
using T05.BirthdayCelebrations.Contracts;

namespace T05.BirthdayCelebrations.Models
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
