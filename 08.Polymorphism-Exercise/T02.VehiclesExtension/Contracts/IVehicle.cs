namespace T02.VehiclesExtension.Contracts
{
    public interface IVehicle
    {
        double FuelQuantity { get; }

        double FuelConsumptionPerKm { get; }

        double TankCapacity { get; }

        bool IsEmpty { get; set; }

        void Drive(double distance);

        void Refuel(double fuel);
    }
}
