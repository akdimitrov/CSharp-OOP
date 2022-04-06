using System;
using System.Collections.Generic;
using System.Linq;
using T05.BirthdayCelebrations.Contracts;
using T05.BirthdayCelebrations.Models;

namespace T05.BirthdayCelebrations
{
    class Program
    {
        static void Main(string[] args)
        {
            List<IBirthable> citizensAndPets = new List<IBirthable>();
            string input = Console.ReadLine();
            while (input != "End")
            {
                string[] info = input.Split();
                string type = info[0];
                if (type == "Citizen")
                {
                    string name = info[1];
                    int age = int.Parse(info[2]);
                    string id = info[3];
                    string birthdate = info[4];
                    citizensAndPets.Add(new Citizen(name, age, id, birthdate));
                }
                else if (type == "Pet")
                {
                    string name = info[1];
                    string birthdate = info[2];
                    citizensAndPets.Add(new Pet(name, birthdate));
                }

                input = Console.ReadLine();
            }

            string year = Console.ReadLine();
            var selected = citizensAndPets.Where(x => x.Birthdate.EndsWith(year)).ToList();
            selected.ForEach(x => Console.WriteLine(x.Birthdate));
        }
    }
}
