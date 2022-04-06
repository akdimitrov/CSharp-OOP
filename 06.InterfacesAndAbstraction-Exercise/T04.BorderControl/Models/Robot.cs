using System;
using System.Collections.Generic;
using System.Text;
using T04.BorderControl.Contracts;

namespace T04.BorderControl.Models
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
