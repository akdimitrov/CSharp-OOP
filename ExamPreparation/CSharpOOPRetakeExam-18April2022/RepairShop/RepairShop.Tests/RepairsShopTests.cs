using System;
using NUnit.Framework;

namespace RepairShop.Tests
{
    public class Tests
    {
        public class RepairsShopTests
        {
            [Test]
            public void Test_Car_Constructor_Should_Initialize()
            {
                Car car1 = new Car("A5", 0);
                Car car2 = new Car("unknown", 5);

                Assert.AreEqual("A5", car1.CarModel);
                Assert.AreEqual(0, car1.NumberOfIssues);
                Assert.IsTrue(car1.IsFixed);

                Assert.AreEqual("unknown", car2.CarModel);
                Assert.AreEqual(5, car2.NumberOfIssues);
                Assert.IsFalse(car2.IsFixed);
            }

            [Test]
            public void Garage_Constructor_Should_Initialize_PositiveTest()
            {
                Garage garage1 = new Garage("Pri Petko", 1);
                Garage garage2 = new Garage("Lux", 99);
                Garage garage3 = new Garage("UniverseGarage", int.MaxValue);

                Assert.AreEqual("Pri Petko", garage1.Name);
                Assert.AreEqual(1, garage1.MechanicsAvailable);
                Assert.AreEqual(0, garage1.CarsInGarage);

                Assert.AreEqual("Lux", garage2.Name);
                Assert.AreEqual(99, garage2.MechanicsAvailable);
                Assert.AreEqual(0, garage2.CarsInGarage);

                Assert.AreEqual("UniverseGarage", garage3.Name);
                Assert.AreEqual(int.MaxValue, garage3.MechanicsAvailable);
                Assert.AreEqual(0, garage3.CarsInGarage);
            }

            [Test]
            public void Garage_Constructor_ShouldThrowExcepstion_WithInvalidParams()
            {
                Assert.Throws<ArgumentException>(() => new Garage("Pri Petko", 0));
                Assert.Throws<ArgumentNullException>(() => new Garage("", 99));
                Assert.Throws<ArgumentNullException>(() => new Garage(null, 99));
                Assert.Throws<ArgumentNullException>(() => new Garage("", 0));
            }

            [Test]
            public void AddCar_ShouldSucceed_IfMechanicsAvailable()
            {
                Garage garage = new Garage("MCar", 2);
                Car car1 = new Car("A5", 8);
                Car car2 = new Car("A6", 5859);

                garage.AddCar(car1);
                garage.AddCar(car2);

                Assert.AreEqual(2, garage.CarsInGarage);
            }

            [Test]
            public void AddCar_ShouldThrowException_IfNoMechanicsAvailable()
            {
                Garage garage = new Garage("MCar", 2);
                Car car1 = new Car("A5", 8);
                Car car2 = new Car("A6", 5859);
                Car car3 = new Car("A7", 0);

                garage.AddCar(car1);
                garage.AddCar(car2);
                Assert.Throws<InvalidOperationException>(() => garage.AddCar(car3));
            }

            [Test]
            public void FixCar_Should_ZeroCarIssues_And_ReturnTheCar()
            {
                Garage garage = new Garage("MCar", 2);
                Car car = new Car("A5", 8);
                garage.AddCar(car);
                var resultCar = garage.FixCar("A5");

                Assert.AreEqual(0, resultCar.NumberOfIssues);
                Assert.AreEqual("A5", resultCar.CarModel);
                Assert.AreEqual(resultCar, car);
                Assert.AreSame(resultCar, car);
            }

            [Test]
            public void FixCar_Should_ThrowExceptionIfCarIsNotInGarage()
            {
                Garage garage = new Garage("MCar", 2);
                Car car = new Car("A5", 8);

                Assert.Throws<InvalidOperationException>(() => garage.FixCar("A5"));
            }

            [Test]
            public void RemoveFixedCar_Should_RemoveAllFixedCars_AndReturnTheirCount()
            {
                Garage garage = new Garage("MCar", 5);
                Car car1 = new Car("A5", 8);
                Car car2 = new Car("A6", 5859);
                Car car3 = new Car("A7", 0);
                Car car4 = new Car("A8", 5);
                garage.AddCar(car1);
                garage.AddCar(car2);
                garage.AddCar(car3);
                garage.AddCar(car4);

                garage.FixCar("A5");
                garage.FixCar("A6");
                garage.FixCar("A7");

                int result = garage.RemoveFixedCar();

                Assert.AreEqual(3, result);
                Assert.AreEqual(1, garage.CarsInGarage);
            }

            [Test]
            public void RemoveFixedCar_Should_ThrowExceptionIfNoFixedCars()
            {
                Garage garage = new Garage("MCar", 16);
                Car car1 = new Car("A5", 8);
                Car car2 = new Car("A6", 5859);
                Car car3 = new Car("A7", 1);
                Car car4 = new Car("A8", 5);

                garage.AddCar(car1);
                garage.AddCar(car2);
                garage.AddCar(car3);

                Assert.Throws<InvalidOperationException>(() => garage.RemoveFixedCar());
            }

            [Test]
            public void Report_ShouldReturn_AccurateInfo()
            {
                Garage garage = new Garage("BaiGosho", 3);
                Car car1 = new Car("A5", 8);
                Car car2 = new Car("A6", 5859);
                Car car3 = new Car("A7", 68);
                Car car4 = new Car("A8", 5);

                garage.AddCar(car1);
                garage.AddCar(car2);
                garage.AddCar(car3);

                garage.FixCar("A5");
                garage.FixCar("A6");
                garage.RemoveFixedCar();

                garage.AddCar(car4);

                string expectedResult = "There are 2 which are not fixed: A7, A8.";
                Assert.AreEqual(expectedResult, garage.Report());
            }
        }
    }
}