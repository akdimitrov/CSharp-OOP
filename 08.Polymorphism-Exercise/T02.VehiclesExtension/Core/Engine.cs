using System;
using System.Collections.Generic;
using System.Linq;
using T02.VehiclesExtension.Contracts;
using T02.VehiclesExtension.Models;

namespace T02.VehiclesExtension.Core
{
    public class Engine
    {
        public void Run()
        {
            double[] carInfo = Console.ReadLine().Split().Skip(1).Select(double.Parse).ToArray();
            double[] truckInfo = Console.ReadLine().Split().Skip(1).Select(double.Parse).ToArray();
            double[] busInfo = Console.ReadLine().Split().Skip(1).Select(double.Parse).ToArray();

            Dictionary<string, IVehicle> vehicles = new Dictionary<string, IVehicle>()
            {
                ["Car"] = new Car(carInfo[0], carInfo[1], carInfo[2]),
                ["Truck"] = new Truck(truckInfo[0], truckInfo[1], truckInfo[2]),
                ["Bus"] = new Bus(busInfo[0], busInfo[1], busInfo[2])
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
                else if (cmdType == "DriveEmpty")
                {
                    vehicles[vehicle].IsEmpty = true;
                    vehicles[vehicle].Drive(value);
                    vehicles[vehicle].IsEmpty = false;
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
