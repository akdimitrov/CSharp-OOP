namespace T01.Vehicles.Contracts
{
    public interface IVehicle
    {
        double FuelQuantity { get; }

        double FuelConsumptionPerKm { get; }

        void Drive(double distance);

        void Refuel(double fuel);
    }
}
