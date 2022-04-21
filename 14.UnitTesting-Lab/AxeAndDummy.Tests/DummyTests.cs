using System;
using NUnit.Framework;

namespace AxeAndDummy.Tests
{
    [TestFixture]
    public class DummyTests
    {
        [Test]
        public void Test_Dummy_Loses_Health_When_Attacked()
        {
            Dummy dummy = new Dummy(100, 100);
            dummy.TakeAttack(10);
            Assert.AreEqual(dummy.Health, 90, "Dummy should loses health when attacked");
        }

        [Test]
        public void Test_Dead_Dummy_With_Zero_Health_Should_Throws_Exception_When_Attacked()
        {
            Dummy dummy = new Dummy(0, 100);
            Assert.Throws<InvalidOperationException>(() => dummy.TakeAttack(10), "Dead Dummy should throws an exception when attacked");
        }

        [Test]
        public void Test_Dead_Dummy_With_Negative_Health_Should_Throws_Exception_When_Attacked()
        {
            Dummy dummy = new Dummy(-1, 100);
            Assert.Throws<InvalidOperationException>(() => dummy.TakeAttack(10), "Dead Dummy should throws an exception when attacked");
        }

        [Test]
        public void Test_Dead_Dummy_With_Zero_Health_Can_Give_Experience()
        {
            Dummy dummy = new Dummy(0, 100);
            var experience = dummy.GiveExperience();
            Assert.AreEqual(experience, 100, "Dead Dummy should give experience");
        }

        [Test]
        public void Test_Dead_Dummy_With_Negative_Health_Can_Give_Experience()
        {
            Dummy dummy = new Dummy(-1, 100);
            var experience = dummy.GiveExperience();
            Assert.AreEqual(experience, 100, "Dead Dummy should give experience");
        }

        [Test]
        public void Test_Alive_Dummy_Cannot_Give_Experience_And_Throws_Exception()
        {
            Dummy dummy = new Dummy(100, 100);
            Assert.Throws<InvalidOperationException>(() => dummy.GiveExperience(), "Dead Dummy cannot give experience");
        }
    }
}