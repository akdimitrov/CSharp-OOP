namespace T02.VehiclesExtension.Models
{
    public class Truck : Vehicle
    {
        public Truck(double fuelQuantity, double fuelConsumptionPerKm, double tankCapacity)
            : base(fuelQuantity, fuelConsumptionPerKm + 1.6, tankCapacity) { }

        public override void Refuel(double fuel)
        {
            if (CanRefuel(fuel))
            {
                base.Refuel(fuel * 0.95);
            }
        }
    }
}
