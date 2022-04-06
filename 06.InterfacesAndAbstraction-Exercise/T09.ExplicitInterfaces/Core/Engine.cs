using System;
using System.Collections.Generic;
using System.Text;
using T09.ExplicitInterfaces.Contracts;
using T09.ExplicitInterfaces.Models;

namespace T09.ExplicitInterfaces.Core
{
    public class Engine
    {
        public void Run()
        {
            string input = Console.ReadLine();
            while (input != "End")
            {
                string[] citizenInfo = input.Split();
                string name = citizenInfo[0];
                string country = citizenInfo[1];
                int age = int.Parse(citizenInfo[2]);

                Citizen citizen = new Citizen(name, country, age);
                Console.WriteLine((citizen as IPerson).GetName());
                Console.WriteLine((citizen as IResident).GetName());

                input = Console.ReadLine();
            }
        }
    }
}
