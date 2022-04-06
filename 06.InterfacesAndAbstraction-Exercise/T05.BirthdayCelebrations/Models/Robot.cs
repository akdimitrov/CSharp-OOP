using System;
using System.Collections.Generic;
using System.Text;
using T05.BirthdayCelebrations.Contracts;

namespace T05.BirthdayCelebrations.Models
{
    public class Robot : IIdentifiable
    {
        public Robot(string model, string id)
        {
            Model = model;
            Id = id;
        }

        public string Model { get; set; }

        public string Id { get; set; }
    }
}
