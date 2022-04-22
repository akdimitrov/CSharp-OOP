using System;
using NUnit.Framework;

namespace AxeAndDummy.Tests
{
    [TestFixture]
    public class AxeTests
    {
        [Test]
        public void Test_AxeLosesDurability_AfterEachAttack()
        {
            Axe axe = new Axe(10, 10);
            Dummy dummy = new Dummy(100, 80);
            for (int i = 0; i < 5; i++)
            {
                axe.Attack(dummy);
            }

            Assert.AreEqual(axe.DurabilityPoints, 5, "Axe should drop durability by one after every attack.");
        }

        [TestCase(10, -1, 100, 80)]
        [TestCase(10, 0, 100, 80)]
        public void Test_AttackingWithBrokenWeapon_ShouldThrowException(int attack, int durability, int health, int experience)
        {
            Axe axe = new Axe(attack, durability);
            Dummy dummy = new Dummy(health, experience);

            Assert.Throws<InvalidOperationException>(() => axe.Attack(dummy), "Axe cannot attack with negative durability.");
        }
    }
}