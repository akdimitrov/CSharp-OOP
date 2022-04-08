using System;
using T02.VehiclesExtension.Contracts;

namespace T02.VehiclesExtension
{
    public abstract class Vehicle : IVehicle
    {
        private double fuelQuantity;

        protected Vehicle(double fuelQuantity, double fuelConsumptionPerKm, double tankCapacity)
        {
            TankCapacity = tankCapacity;
            FuelConsumptionPerKm = fuelConsumptionPerKm;
            FuelQuantity = fuelQuantity;
        }

        public double TankCapacity { get; }

        public virtual double FuelConsumptionPerKm { get; protected set; }

        public double FuelQuantity
        {
            get => fuelQuantity;
            protected set
            {
                if (value > TankCapacity)
                {
                    value = 0;
                }

                fuelQuantity = value;
            }
        }

        public bool IsEmpty { get; set; }

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
            if (CanRefuel(fuel))
            {
                FuelQuantity += fuel;
            }
        }

        public override string ToString()
        {
            return $"{GetType().Name}: {FuelQuantity:f2}";
        }

        protected bool CanRefuel(double fuel)
        {
            if (fuel <= 0)
            {
                Console.WriteLine("Fuel must be a positive number");
                return false;
            }
            else if (FuelQuantity + fuel > TankCapacity)
            {
                Console.WriteLine($"Cannot fit {fuel} fuel in the tank");
                return false;
            }

            return true;
        }
    }
}
