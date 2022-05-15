using System;
using NUnit.Framework;

namespace SmartphoneShop.Tests
{
    [TestFixture]
    public class SmartphoneShopTests
    {
        [Test]
        public void SmartphoneConstructor_ShouldInitializeProperly()
        {
            Smartphone smartphone1 = new Smartphone("Nokia 3310", 100);
            Smartphone smartphone2 = new Smartphone("Siemens A50", 0);

            Assert.AreEqual("Nokia 3310", smartphone1.ModelName);
            Assert.AreEqual(100, smartphone1.MaximumBatteryCharge);
            Assert.AreEqual(100, smartphone1.CurrentBateryCharge);

            Assert.AreEqual("Siemens A50", smartphone2.ModelName);
            Assert.AreEqual(0, smartphone2.MaximumBatteryCharge);
            Assert.AreEqual(0, smartphone2.CurrentBateryCharge);
        }

        [Test]
        public void ShopConstructor_ShouldInitializeProperly_WithValidCapacity()
        {
            Shop shop1 = new Shop(0);
            Shop shop2 = new Shop(int.MaxValue);

            Assert.AreEqual(0, shop1.Capacity);
            Assert.AreEqual(int.MaxValue, shop2.Capacity);

            Assert.AreEqual(0, shop1.Count);
            Assert.AreEqual(0, shop2.Count);
        }

        [Test]
        public void ShopConstructor_ShouldThrowException_WithNegativeCapacity()
        {
            Assert.Throws<ArgumentException>(() => new Shop(-1));
            Assert.Throws<ArgumentException>(() => new Shop(int.MinValue));
        }

        [Test]
        public void AddMethod_ShoudAddSmarthphoneToTheCollection_PositiveTest()
        {
            Shop shop = new Shop(5);
            Smartphone smartphone = new Smartphone("Alcatel", 98);
            shop.Add(smartphone);

            Assert.AreEqual(1, shop.Count);
        }

        [Test]
        public void AddMethod_ShoudShouldThrowException_IfPhoneIsAddedAlready()
        {
            Shop shop = new Shop(5);
            Smartphone smartphone = new Smartphone("Alcatel", 98);
            shop.Add(smartphone);

            Assert.Throws<InvalidOperationException>(() => shop.Add(smartphone));
        }

        [Test]
        public void AddMethod_ShoudShouldThrowException_IfCapacityIsFull()
        {
            Shop shop = new Shop(1);
            Smartphone smartphone = new Smartphone("Alcatel", 98);
            shop.Add(smartphone);

            Assert.Throws<InvalidOperationException>(() => shop.Add(smartphone));
        }

        [Test]
        public void RemoveMethod_ShoudRemoveSmarthphone_IfExist()
        {
            Shop shop = new Shop(5);
            Smartphone smartphone = new Smartphone("Alcatel", 98);
            shop.Add(smartphone);
            shop.Remove("Alcatel");

            Assert.AreEqual(0, shop.Count);
        }

        [Test]
        public void RemoveMethod_ShoudShouldThrowException_IfPhoneDoesNotExist()
        {
            Shop shop = new Shop(5);
            Smartphone smartphone = new Smartphone("Alcatel", 98);

            Assert.Throws<InvalidOperationException>(() => shop.Remove("Nokia"));
        }

        [Test]
        public void TestPhoneMethod_ShouldDrainBattery_Positivetest()
        {
            Shop shop = new Shop(5);
            Smartphone smartphone1 = new Smartphone("Samsung", 100);
            Smartphone smartphone2 = new Smartphone("unknown", 100);
            Smartphone smartphone3 = new Smartphone("Huawei", 1);
            shop.Add(smartphone1);
            shop.Add(smartphone2);
            shop.Add(smartphone3);
            shop.TestPhone("Samsung", 64);
            shop.TestPhone("unknown", 0);
            shop.TestPhone("Huawei", 1);

            Assert.AreEqual(36, smartphone1.CurrentBateryCharge);
            Assert.AreEqual(100, smartphone2.CurrentBateryCharge);
            Assert.AreEqual(0, smartphone3.CurrentBateryCharge);
        }

        [Test]
        public void TestPhoneMethod_ShoudShouldThrowException_IfPhoneDoesNotExist()
        {
            Shop shop = new Shop(5);
            Smartphone smartphone = new Smartphone("Alcatel", 98);

            Assert.Throws<InvalidOperationException>(() => shop.TestPhone("Alcatel", 5));
        }

        [Test]
        public void TestPhoneMethod_ShoudShouldThrowException_IfBatteryUsageIsHigherThanCurrentCharge()
        {
            Shop shop = new Shop(5);
            Smartphone smartphone = new Smartphone("Alcatel", 98);
            shop.Add(smartphone);

            Assert.Throws<InvalidOperationException>(() => shop.TestPhone("Alcatel", 99));
        }

        [Test]
        public void ChargePhone_ShouldIncreseBatteryChargeToMaximum_Positivetest()
        {
            Shop shop = new Shop(5);
            Smartphone smartphone1 = new Smartphone("Samsung", 100);
            Smartphone smartphone2 = new Smartphone("unknown", 100);
            Smartphone smartphone3 = new Smartphone("Huawei", 1);
            shop.Add(smartphone1);
            shop.Add(smartphone2);
            shop.Add(smartphone3);
            shop.TestPhone("Samsung", 64);
            shop.TestPhone("unknown", 0);
            shop.TestPhone("Huawei", 1);

            shop.ChargePhone("Samsung");
            shop.ChargePhone("unknown");
            shop.ChargePhone("Huawei");

            Assert.AreEqual(100, smartphone1.CurrentBateryCharge);
            Assert.AreEqual(100, smartphone2.CurrentBateryCharge);
            Assert.AreEqual(1, smartphone3.CurrentBateryCharge);
        }

        [Test]
        public void ChargePhone_ShoudShouldThrowException_IfPhoneDoesNotExist()
        {
            Shop shop = new Shop(5);
            Smartphone smartphone = new Smartphone("Alcatel", 98);

            Assert.Throws<InvalidOperationException>(() => shop.ChargePhone("Alcatel"));
        }
    }
}