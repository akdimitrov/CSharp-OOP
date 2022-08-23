namespace Aquariums.Tests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class AquariumsTests
    {
        [Test]
        public void FishConstructorShouldWorkProperly()
        {
            Fish fish = new Fish("Nemo");

            Assert.AreEqual("Nemo", fish.Name);
            Assert.IsTrue(fish.Available);
        }

        [Test]
        public void AquariumConstructorShouldInitializeProperly()
        {
            Aquarium aquarium = new Aquarium("Big", 10);

            Assert.AreEqual("Big", aquarium.Name);
            Assert.AreEqual(10, aquarium.Capacity);
            Assert.AreEqual(0, aquarium.Count);
        }

        [Test]
        public void AquariumConstructorShouldThrowExceptionWihtInvlidName()
        {
            Assert.Throws<ArgumentNullException>(() => new Aquarium("", 10));
            Assert.Throws<ArgumentNullException>(() => new Aquarium(null, 10));
        }

        [Test]
        public void AquariumConstructorShouldThrowExceptionWihNegativeCapacity()
        {
            Assert.Throws<ArgumentException>(() => new Aquarium("Big", -1));
        }

        [Test]
        public void AddFishShouldWokrProperly()
        {
            Aquarium aquarium = new Aquarium("Big", 10);
            Fish fish1 = new Fish("Nemo");
            Fish fish2 = new Fish("Not Nemo");

            aquarium.Add(fish1);
            aquarium.Add(fish2);

            Assert.AreEqual(2, aquarium.Count);
        }

        [Test]
        public void AddFishShouldShouldThrowExceptionIfCapcityIsFull()
        {
            Aquarium aquarium = new Aquarium("Big", 1);
            Fish fish1 = new Fish("Nemo");
            Fish fish2 = new Fish("Not Nemo");

            aquarium.Add(fish1);

            Assert.Throws<InvalidOperationException>(() => aquarium.Add(fish2));
        }

        [Test]
        public void SellFishShouldSetFishAvailabilityToFalseAndRetunIt()
        {
            Aquarium aquarium = new Aquarium("Big", 10);
            Fish fish1 = new Fish("Nemo");
            Fish fish2 = new Fish("Not Nemo");

            aquarium.Add(fish1);
            aquarium.Add(fish2);

            var soldFish = aquarium.SellFish("Nemo");

            Assert.AreEqual(fish1, soldFish);
            Assert.IsFalse(soldFish.Available);
        }

        [Test]
        public void SellFishShouldThrowExceptionIfFishNotExist()
        {
            Aquarium aquarium = new Aquarium("Big", 10);
            Fish fish1 = new Fish("Nemo");
            Fish fish2 = new Fish("Not Nemo");

            aquarium.Add(fish1);
            aquarium.Add(fish2);

            Assert.Throws<InvalidOperationException>(() => aquarium.SellFish("Gogo"));
        }

        [Test]
        public void ReportShouldReturnAppropriateResult()
        {
            Aquarium aquarium = new Aquarium("Big", 10);
            Fish fish1 = new Fish("Nemo");
            Fish fish2 = new Fish("Not Nemo");
            Fish fish3 = new Fish("Nemo2");

            aquarium.Add(fish1);
            aquarium.Add(fish2);
            aquarium.Add(fish3);

            var fishNames = $"{fish1.Name}, {fish2.Name}, {fish3.Name}";
            var expectedResult = $"Fish available at {aquarium.Name}: {fishNames}";

            var actualtResult = aquarium.Report();

            Assert.AreEqual(expectedResult, actualtResult);
        }
    }
}
