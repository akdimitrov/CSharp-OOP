namespace FightingArena.Tests
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;

    [TestFixture]
    public class WarriorTests
    {
        [TestCase("Goran", 100, 100)]
        [TestCase("A", int.MaxValue, int.MaxValue)]
        [TestCase("ASKJmoksmax342r.d;[4skomxlsakMAKLMsklmsdaslxm lcdscsd", 1, 0)]
        public void Constructor_ShouldPass_WithValidData(string name, int damage, int hp)
        {
            Warrior warrior = new Warrior(name, damage, hp);
            Assert.IsNotNull(warrior);
        }

        [TestCase("", 100, 100)]
        [TestCase(" ", int.MaxValue, int.MaxValue)]
        [TestCase(null, int.MaxValue, int.MaxValue)]
        [TestCase("WWW", 0, 100)]
        [TestCase("Zulu", int.MinValue, int.MaxValue)]
        [TestCase("Monster", -1, -1)]
        [TestCase("Zulu", int.MaxValue, int.MinValue)]
        [TestCase("Zulu", int.MaxValue, -1)]
        public void Constructor_ShouldThrow_ArgumentException_WithInvalidData(string name, int damage, int hp)
        {
            Assert.Throws<ArgumentException>(() => new Warrior(name, damage, hp));
        }

        [TestCase("Goran", 100, 32)]
        [TestCase("A", int.MaxValue, int.MaxValue)]
        [TestCase("Mitko", 1, 100)]
        public void Attack_ShouldDecreaseHp_WithValidData(string name, int damage, int hp)
        {
            Warrior attacker = new Warrior(name, damage, hp);
            Warrior defender = new Warrior("Zdravko", 31, 50);

            int expectedAttackerHp = attacker.HP - defender.Damage;
            int expectedDefenderHp = Math.Max(0, defender.HP - attacker.Damage);
            attacker.Attack(defender);

            Assert.AreEqual(expectedAttackerHp, attacker.HP);
            Assert.AreEqual(expectedDefenderHp, defender.HP);
        }

        [TestCaseSource("TestCaseInvalidAttackData")]
        public void Attack_ShouldThrow_InvalidOperationException_WithInvalidData(Warrior attacker, Warrior defender)
        {
            Assert.Throws<InvalidOperationException>(() => attacker.Attack(defender));
        }

        public static IEnumerable<TestCaseData> TestCaseInvalidAttackData()
        {
            TestCaseData[] testCases = new TestCaseData[]
            {
                new TestCaseData(new Warrior("Barberian", 50, 5), new Warrior("ScaryOne", 50,50)),
                new TestCaseData(new Warrior("Barberian", 50, 100), new Warrior("ScaryOne", 50,5)),
                new TestCaseData(new Warrior("Barberian", 50, 50), new Warrior("ScaryOne", 100,100))
            };

            foreach (var testCase in testCases)
            {
                yield return testCase;
            }
        }
    }
}