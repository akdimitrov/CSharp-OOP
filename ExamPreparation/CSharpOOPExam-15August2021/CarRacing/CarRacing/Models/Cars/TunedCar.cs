using System;

namespace CarRacing.Models.Cars
{
    public class TunedCar : Car
    {
        private const double TunedCarAvailableFuel = 65;
        private const double TunedCarFuelConsumptionPerRace = 7.5;

        public TunedCar(string make, string model, string VIN, int horsePower) : base(make, model, VIN, horsePower, TunedCarAvailableFuel, TunedCarFuelConsumptionPerRace)
        {
        }

        public override void Drive()
        {
            base.Drive();
            this.HorsePower = (int)Math.Round(base.HorsePower * 0.97);
        }
    }
}
