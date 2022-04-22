using System;
using NUnit.Framework;

namespace AxeAndDummy.Tests
{
    [TestFixture]
    public class DummyTests
    {
        [Test]
        public void Test_AliveDummy_LosesHealth_WhenAttacked()
        {
            Dummy dummy = new Dummy(100, 100);
            dummy.TakeAttack(10);
            Assert.AreEqual(dummy.Health, 90, "Dummy should lose health when attacked");
        }

        [TestCase(0, 100)]
        [TestCase(-1, 100)]
        public void Test_DeadDummy_ShouldThrowException_WhenAttacked(int health, int experience)
        {
            Dummy dummy = new Dummy(health, experience);
            Assert.Throws<InvalidOperationException>(() => dummy.TakeAttack(10), "Dead Dummy should throw an exception when attacked");
        }

        [TestCase(0, 100)]
        [TestCase(-1, 100)]
        public void Test_DeadDummy_ShouldGiveExperience(int health, int experience)
        {
            Dummy dummy = new Dummy(health, experience);
            var resultXp = dummy.GiveExperience();
            Assert.AreEqual(resultXp, 100, "Dead Dummy should give experience");
        }

        [Test]
        public void Test_AliveDummy_CannotGiveExperience_AndThrowsException()
        {
            Dummy dummy = new Dummy(100, 100);
            Assert.Throws<InvalidOperationException>(() => dummy.GiveExperience(), "Alive Dummy cannot give experience");
        }
    }
}