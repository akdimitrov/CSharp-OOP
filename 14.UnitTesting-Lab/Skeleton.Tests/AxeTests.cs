using System;
using NUnit.Framework;

namespace Skeleton.Tests
{
    [TestFixture]
    public class AxeTests
    {
        [Test]
        public void Test_Axe_Loses_Durability_After_Each_Attack()
        {
            Axe axe = new Axe(10, 10);
            Dummy dummy = new Dummy(100, 80);
            for (int i = 0; i < 5; i++)
            {
                axe.Attack(dummy);
            }

            Assert.AreEqual(axe.DurabilityPoints, 5, "Axe should drop durability by one after every attack.");
        }

        [Test]
        public void Test_Attacking_With_Broken_Weapon_With_Negative_Durability_Should_Throw_Exception()
        {
            Axe axe = new Axe(10, -1);
            Dummy dummy = new Dummy(100, 80);

            Assert.Throws<InvalidOperationException>(() => axe.Attack(dummy), "Axe cannot attack with negative durability.");
        }

        [Test]
        public void Test_Attacking_With_Broken_Weapon_With_Zero_Durability_Should_Throw_Exception()
        {
            Axe axe = new Axe(10, 0);
            Dummy dummy = new Dummy(100, 80);

            Assert.Throws<InvalidOperationException>(() => axe.Attack(dummy), "Axe cannot attack with zero durability.");
        }
    }
}