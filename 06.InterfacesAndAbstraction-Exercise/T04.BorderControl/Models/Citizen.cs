﻿using System;
using System.Collections.Generic;
using System.Text;
using T04.BorderControl.Contracts;

namespace T04.BorderControl.Models
{
    public class Citizen : IIdentifiable
    {
        public Citizen(string name, int age, string id)
        {
            Name = name;
            Age = age;
            Id = id;
        }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Id { get; set; }
    }
}
