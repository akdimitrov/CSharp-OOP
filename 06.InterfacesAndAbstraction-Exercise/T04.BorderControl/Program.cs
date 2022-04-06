using System;
using System.Collections.Generic;
using System.Linq;
using T04.BorderControl.Contracts;
using T04.BorderControl.Models;

namespace T04.BorderControl
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<IIdentifiable> citizensAndRobots = new List<IIdentifiable>();
            string input = Console.ReadLine();
            while (input != "End")
            {
                string[] info = input.Split();
                if (info.Length == 3)
                {
                    string name = info[0];
                    int age = int.Parse(info[1]);
                    string id = info[2];
                    citizensAndRobots.Add(new Citizen(name, age, id));
                }
                else if (info.Length == 2)
                {
                    string model = info[0];
                    string id = info[1];
                    citizensAndRobots.Add(new Robot(model, id));
                }

                input = Console.ReadLine();
            }

            string fakeIdEnd = Console.ReadLine();
            var detained = citizensAndRobots.Where(x => x.Id.EndsWith(fakeIdEnd)).ToList();
            detained.ForEach(x => Console.WriteLine(x.Id));
        }
    }
}
