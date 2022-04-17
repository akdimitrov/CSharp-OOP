namespace FightingArena.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;

    [TestFixture]
    public class ArenaTests
    {
        [Test]
        public void Constructor_ShouldInitializeData()
        {
            Arena arena = new Arena();
            Assert.IsNotNull(arena.Warriors);
            Assert.AreEqual(0, arena.Count);
            Assert.AreEqual(arena.Count, arena.Warriors.Count);
        }

        [TestCaseSource("TestCaseAddingWarriorsPositiveTest")]
        public void Enroll_ShouldAddWarrior_IfIsNotAlreadyAdded
            (Warrior[] warriorsToAdd, string warriorNameToCheck, int expectedCount)
        {
            Arena arena = new Arena();
            foreach (var warrior in warriorsToAdd)
            {
                arena.Enroll(warrior);
            }

            Assert.AreEqual(expectedCount, arena.Count);
            Assert.True(arena.Warriors.Any(x => x.Name == warriorNameToCheck));
        }

        [TestCaseSource("TestCaseAddingWarriorsNegativeTest")]
        public void Enroll_ShouldThrow_InvalidOperationException_WhenWarriorToAdd_AlreadyExists
            (Warrior[] warriors, Warrior warriorToAdd)
        {
            Arena arena = new Arena();
            foreach (var warrior in warriors)
            {
                arena.Enroll(warrior);
            }

            Assert.Throws<InvalidOperationException>(() => arena.Enroll(warriorToAdd));
        }

        [TestCaseSource("TestCaseFightWithValidData")]
        public void Fight_ShouldReduceHp_WithValidData(
            Warrior attacker,
            Warrior defender,
            string attackerName,
            string defenderName,
            int expectedAttackerHp,
            int exprectedDefenderHp)
        {
            Arena arena = new Arena();
            arena.Enroll(attacker);
            arena.Enroll(defender);

            arena.Fight(attackerName, defenderName);

            Assert.AreEqual(expectedAttackerHp, attacker.HP);
            Assert.AreEqual(exprectedDefenderHp, defender.HP);
        }

        [TestCaseSource("TestCaseFightWithInvalidData")]
        public void Fight_ShouldThrow_InvalidOperationException_WithInvalidData(
            Warrior[] warriors,
            string attackerName,
            string defenderName)
        {
            Arena arena = new Arena();
            foreach (var warrior in warriors)
            {
                arena.Enroll(warrior);
            }

            Assert.Throws<InvalidOperationException>(() => arena.Fight(attackerName, defenderName));
        }

        public static IEnumerable<TestCaseData> TestCaseFightWithInvalidData()
        {
            TestCaseData[] testCases = new TestCaseData[]
            {
                new TestCaseData(
                    new Warrior[]{},
                    "One",
                    "Three"),

                new TestCaseData(
                    new Warrior[]{new Warrior("One", 100, 100),new Warrior("Two", 50, 100)},
                    "One",
                    "Three"),

                new TestCaseData(
                    new Warrior[]{new Warrior("Two", 50, 100),new Warrior("One", 100, 100)},
                    "Five",
                    "One"),
            };

            foreach (var testCase in testCases)
            {
                yield return testCase;
            }
        }

        public static IEnumerable<TestCaseData> TestCaseFightWithValidData()
        {
            TestCaseData[] testCases = new TestCaseData[]
            {
                new TestCaseData(
                    new Warrior("One", 100, 100),
                    new Warrior("Two", 50, 100),
                    "One",
                    "Two",
                    50,
                    0),

                new TestCaseData(
                    new Warrior("Two", 50, 100),
                    new Warrior("One", 100, 100),
                    "Two",
                    "One",
                    0,
                    50),

                new TestCaseData(
                    new Warrior("Three", 158, 81),
                    new Warrior("Four", 80, 80),
                    "Three",
                    "Four",
                    1,
                    0)
            };

            foreach (var testCase in testCases)
            {
                yield return testCase;
            }
        }

        public static IEnumerable<TestCaseData> TestCaseAddingWarriorsPositiveTest()
        {
            yield return new TestCaseData(
                new Warrior[]
                {
                    new Warrior("One", 100, 100),
                    new Warrior("Two", 50, 100),
                    new Warrior("Three", 158, 34)
                },
                    "One",
                    3);
        }

        public static IEnumerable<TestCaseData> TestCaseAddingWarriorsNegativeTest()
        {
            yield return new TestCaseData(
                new Warrior[]
                {
                    new Warrior("One", 100, 100),
                    new Warrior("Two", 50, 100),
                    new Warrior("Three", 158, 34)
                },
                    new Warrior("Three", 158, 34)
                );
        }
    }
}
