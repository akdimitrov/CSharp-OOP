namespace T02.VehiclesExtension.Models
{
    public class Car : Vehicle
    {
        public Car(double fuelQuantity, double fuelConsumptionPerKm, double tankCapacity)
            : base(fuelQuantity, fuelConsumptionPerKm + 0.9, tankCapacity) { }
    }
}
