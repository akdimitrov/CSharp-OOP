namespace T02.VehiclesExtension.Models
{
    public class Bus : Vehicle
    {
        public Bus(double fuelQuantity, double fuelConsumptionPerKm, double tankCapacity)
            : base(fuelQuantity, fuelConsumptionPerKm, tankCapacity) { }

        public override double FuelConsumptionPerKm => IsEmpty ?
            base.FuelConsumptionPerKm : base.FuelConsumptionPerKm + 1.4;
    }
}
