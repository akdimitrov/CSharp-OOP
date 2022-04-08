namespace T01.Vehicles.Models
{
    public class Truck : Vehicle
    {
        public Truck(double fuelQuantity, double fuelConsumptionPerKm)
            : base(fuelQuantity, fuelConsumptionPerKm + 1.6) { }

        public override void Refuel(double fuel)
        {
           FuelQuantity += fuel * 0.95;
        }
    }
}
