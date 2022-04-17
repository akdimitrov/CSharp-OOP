namespace CarManager.Tests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class CarManagerTests
    {
        [TestCase("BMW", "M5", 12.5, double.MaxValue)]
        [TestCase("Audi", "A5", 0.1, 0.1)]
        public void Constructor_ShouldPass_WithValidData
            (string make, string model, double fuelConsumption, double fuelCapacity)
        {
            Car car = new Car(make, model, fuelConsumption, fuelCapacity);

            Assert.AreEqual(0, car.FuelAmount);
            Assert.AreEqual(make, car.Make);
            Assert.AreEqual(model, car.Model);
            Assert.AreEqual(fuelConsumption, car.FuelConsumption);
            Assert.AreEqual(fuelCapacity, car.FuelCapacity);
        }

        [TestCase("", "M5", 12.5, 1000)]
        [TestCase(null, "M5", 12.5, 452)]
        [TestCase("Volvo", "", 12.5, 2)]
        [TestCase("Volvo", null, 12.5, 22)]
        [TestCase("Citroen", "C4", 0, 999)]
        [TestCase("Mercedes", "Sclass", -1, double.MaxValue)]
        [TestCase("Bentley", "Flying Spur", 0.22, 0)]
        [TestCase("Trabant", "The Best", 18.3, -1)]
        public void Constructor_ShouldThrow_ArgumentException_WithInvalidData
            (string make, string model, double fuelConsumption, double fuelCapacity)
        {
            Assert.Throws<ArgumentException>(() => new Car(make, model, fuelConsumption, fuelCapacity));
        }

        [TestCase("Audi", "A5", 8.5, 75, 0.1)]
        [TestCase("Audi", "A5", 8.5, 75, 75)]
        [TestCase("Audi", "A5", 8.5, 75, 33.5)]
        public void Refuel_ShouldSucceed_WithValidData
            (string make, string model, double fuelConsumption, double fuelCapacity, double fuelToRefuel)
        {
            Car car = new Car(make, model, fuelConsumption, fuelCapacity);
            car.Refuel(fuelToRefuel);
            Assert.AreEqual(fuelToRefuel, car.FuelAmount);
        }

        [TestCase("Audi", "A5", 8.5, 2156123789, double.MaxValue)]
        [TestCase("Audi", "A5", 8.5, 0.1, 0.2)]
        public void Refuel_ShouldSet_FuelAmount_ToFuelCapacity_WhenFuelToRefuelIsOverTheCapacity
            (string make, string model, double fuelConsumption, double fuelCapacity, double fuelToRefuel)
        {
            Car car = new Car(make, model, fuelConsumption, fuelCapacity);
            car.Refuel(fuelToRefuel);
            Assert.AreEqual(car.FuelCapacity, car.FuelAmount);
        }

        [TestCase("Audi", "A5", 8.5, double.MaxValue, double.MinValue)]
        [TestCase("Audi", "A5", 8.5, 0.1, 0)]
        [TestCase("Audi", "A5", 8.5, 75, -0.1)]
        public void Refuel_ShouldThrow_ArgumentException_WhenFuelToRefuel_IsZeroOrNegative
            (string make, string model, double fuelConsumption, double fuelCapacity, double fuelToRefuel)
        {
            Car car = new Car(make, model, fuelConsumption, fuelCapacity);
            Assert.Throws<ArgumentException>(() => car.Refuel(fuelToRefuel));
        }

        [TestCase("Bmw", "Z3", 6.4, 75, 6.4, 100)]
        [TestCase("Hummer", "H3", 100, double.MaxValue, double.MaxValue, double.MaxValue)]
        [TestCase("Lada", "1200", 99.9, 100, 100.1, 100)]
        [TestCase("Lada", "1200", 99.9, 100, 100.1, 0)]
        [TestCase("Lada", "1200", 3.5, 10, 0.1, 0)]
        public void Drive_ShouldDecrese_FuelAmount_WhenCarHasEnoughFuel
            (string make, string model, double fuelConsumption, double fuelCapacity, double fuelToRefuel, double distance)
        {
            Car car = new Car(make, model, fuelConsumption, fuelCapacity);
            double fuelNeeded = (distance / 100) * car.FuelConsumption;

            car.Refuel(fuelToRefuel);
            double expectedFuelAmountResult = car.FuelAmount - fuelNeeded;

            car.Drive(distance);
            Assert.AreEqual(expectedFuelAmountResult, car.FuelAmount);
        }

        [TestCase("Bmw", "Z3", 6.4, 75, 63.9, 1000)]
        [TestCase("Hummer", "H3", double.MaxValue, double.MaxValue, double.MaxValue, double.MaxValue)]
        [TestCase("Lada", "1200", 100.1, 100, 100.1, 100)]
        public void Drive_ShouldtThrow_InvalidOperationException_WhenCarDoesNotHaveEnoughFuel
            (string make, string model, double fuelConsumption, double fuelCapacity, double fuelToRefuel, double distance)
        {
            Car car = new Car(make, model, fuelConsumption, fuelCapacity);
            car.Refuel(fuelToRefuel);
            Assert.Throws<InvalidOperationException>(() => car.Drive(distance));
        }
    }
}