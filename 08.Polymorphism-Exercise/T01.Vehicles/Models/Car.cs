namespace T01.Vehicles.Models
{
    public class Car : Vehicle
    {
        public Car(double fuelQuantity, double fuelConsumptionPerKm)
            : base(fuelQuantity, fuelConsumptionPerKm + 0.9) { }
    }
}
