using System;
using System.Collections.Generic;
using T01.Vehicles.Contracts;
using T01.Vehicles.Models;

namespace T01.Vehicles.Core
{
    public class Engine
    {
        public void Run()
        {
            string[] carInfo = Console.ReadLine().Split();
            string[] truckInfo = Console.ReadLine().Split();

            Dictionary<string, IVehicle> vehicles = new Dictionary<string, IVehicle>()
            {
                ["Car"] = new Car(double.Parse(carInfo[1]), double.Parse(carInfo[2])),
                ["Truck"] = new Truck(double.Parse(truckInfo[1]), double.Parse(truckInfo[2]))
            };

            int commands = int.Parse(Console.ReadLine());
            for (int i = 0; i < commands; i++)
            {
                string[] cmd = Console.ReadLine().Split();
                string cmdType = cmd[0];
                string vehicle = cmd[1];
                double value = double.Parse(cmd[2]);

                if (cmdType == "Drive")
                {
                    vehicles[vehicle].Drive(value);
                }
                else if (cmdType == "Refuel")
                {
                    vehicles[vehicle].Refuel(value);
                }
            }

            foreach (var (_, vehicle) in vehicles)
            {
                Console.WriteLine(vehicle);
            }
        }
    }
}
