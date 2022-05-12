namespace Presents.Tests
{
    using System;
    using System.Linq;
    using NUnit.Framework;

    [TestFixture]
    public class PresentsTests
    {
        private Bag bag;

        [SetUp]
        public void Initialize()
        {
            this.bag = new Bag();
        }

        [Test]
        public void PresentContstructor_SHouldInitialize_Present()
        {
            Present present = new Present("Car", 15.5);
            Assert.IsNotNull(present);
            Assert.AreEqual(15.5, present.Magic);
            Assert.AreEqual("Car", present.Name);
        }

        [Test]
        public void BagContstructor_SHouldInitialize_Bag()
        {
            Assert.IsNotNull(bag);
            Assert.IsNotNull(bag.GetPresents());
            Assert.AreEqual(0, bag.GetPresents().Count);
        }

        [Test]
        public void Create_ShouldThrowException_IfPresentIsNull()
        {
            Present present = null;
            Assert.Throws<ArgumentNullException>(() => bag.Create(present));
        }

        [Test]
        public void Create_ShouldThrowException_IfPresentAlereadtExistInTheBag()
        {
            Present present = new Present("Toy", 2.4);
            bag.Create(present);
            Assert.Throws<InvalidOperationException>(() => bag.Create(present));
        }

        [Test]
        public void Create_ShoudAddPresentToBag_And_ReturnMessage()
        {
            Present present = new Present("Toy", 2.4);
            string result = bag.Create(present);
            string expected = $"Successfully added present {present.Name}.";

            Assert.AreEqual(1, bag.GetPresents().Count);
            Assert.That(bag.GetPresents().Contains(present));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Remove_ShouldRemovePresent_And_ReturnTrueIfPresentExistedAndWasRemoved()
        {
            Present present = new Present("Toy", 2.4);
            Present notAddedPresent = new Present("Truck", 4.7);
            string result = bag.Create(present);

            Assert.IsTrue(bag.Remove(present));
            Assert.IsFalse(bag.Remove(notAddedPresent));
            Assert.AreEqual(0, bag.GetPresents().Count);
        }

        [Test]
        public void GetPresentWithLeastMagic_ShouldReturnTheRightPresent()
        {
            Present present1 = new Present("Toy", 2.4);
            Present present2 = new Present("Truck", 89.7);
            Present present3 = new Present("Ball", 14.7);
            Present present4 = new Present("Nothing", 0);

            bag.Create(present1);
            bag.Create(present2);
            bag.Create(present3);
            bag.Create(present4);

            var result = bag.GetPresentWithLeastMagic();

            Assert.AreEqual(present4, result);
            Assert.AreSame(present4, result);
            Assert.AreEqual(0, result.Magic);
            Assert.AreEqual("Nothing", result.Name);
        }

        [Test]
        public void GetPresent_ShouldReturnPresentByName_Or_Null_IfItDoesNotExist()
        {
            Present present1 = new Present("Toy", 2.4);

            bag.Create(present1);

            var result = bag.GetPresent("Toy");
            var nullResult = bag.GetPresent("Puzzle");

            Assert.AreEqual(present1, result);
            Assert.AreSame(present1, result);
            Assert.AreEqual(2.4, result.Magic);
            Assert.AreEqual("Toy", result.Name);
            Assert.IsNull(nullResult);
        }
    }
}
