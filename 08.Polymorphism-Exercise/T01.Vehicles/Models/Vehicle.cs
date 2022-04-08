using System;
using T01.Vehicles.Contracts;

namespace T01.Vehicles.Models
{
    public abstract class Vehicle : IVehicle
    {
        protected Vehicle(double fuelQuantity, double fuelConsumptionPerKm)
        {
            FuelQuantity = fuelQuantity;
            FuelConsumptionPerKm = fuelConsumptionPerKm;
        }

        public double FuelQuantity { get; protected set; }

        public double FuelConsumptionPerKm { get; private set; }

        public void Drive(double distance)
        {
            double fuelNeeded = distance * FuelConsumptionPerKm;
            if (fuelNeeded > FuelQuantity)
            {
                Console.WriteLine($"{GetType().Name} needs refueling");
                return;
            }

            FuelQuantity -= fuelNeeded;
            Console.WriteLine($"{GetType().Name} travelled {distance} km");
        }

        public virtual void Refuel(double fuel)
        {
            FuelQuantity += fuel;
        }

        public override string ToString()
        {
            return $"{GetType().Name}: {FuelQuantity:f2}";
        }
    }
}
