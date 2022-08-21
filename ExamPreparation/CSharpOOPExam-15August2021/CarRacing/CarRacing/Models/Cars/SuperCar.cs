namespace CarRacing.Models.Cars
{
    public class SuperCar : Car
    {
        private const double SuperCarAvailableFuel = 80;
        private const double SuperCarFuelConsumptionPerRace = 10;

        public SuperCar(string make, string model, string VIN, int horsePower) : base(make, model, VIN, horsePower, SuperCarAvailableFuel, SuperCarFuelConsumptionPerRace)
        {
        }
    }
}
