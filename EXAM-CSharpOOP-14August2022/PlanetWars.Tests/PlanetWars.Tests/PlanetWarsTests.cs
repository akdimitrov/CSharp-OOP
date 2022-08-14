using NUnit.Framework;
using System;
using System.ComponentModel;
using System.Linq;

namespace PlanetWars.Tests
{
    public class Tests
    {
        [TestFixture]
        public class PlanetWarsTests
        {
            [Test]
            public void ConstructorShouldPassWithValidData()
            {
                string name = "Earth";
                double budget = 12.5;
                var planet = new Planet(name, budget);

                Assert.That(planet != null);
                Assert.That(planet.Weapons != null);
                Assert.That(planet.Weapons.Count == 0);
                Assert.AreEqual(name, planet.Name);
                Assert.AreEqual(budget, planet.Budget);
            }

            [Test]
            public void ConstructorShouldThrowExceptionWithInvalidPlanetName()
            {
                Assert.Throws<ArgumentException>(() => new Planet("", 20));
                Assert.Throws<ArgumentException>(() => new Planet(null, 18.6));
            }

            [Test]
            public void ConstructorShouldThrowExceptionWithInvalidPlanetBudget()
            {
                Assert.Throws<ArgumentException>(() => new Planet("Earth", -1));
            }

            [Test]
            public void ProfitShouldIncreaseBudget()
            {
                var planet = new Planet("Mars", 10);
                planet.Profit(12.5);

                Assert.AreEqual(22.5, planet.Budget);
            }

            [Test]
            public void SpendFundsShouldDecreaseBudget()
            {
                var planet = new Planet("Mars", 10);
                planet.SpendFunds(8.5);

                Assert.AreEqual(1.5, planet.Budget);
            }

            [Test]
            public void SpendFundsShouldThrowExceptionIfAmountIsHigherThanBudget()
            {
                var planet = new Planet("Mars", 10);

                Assert.Throws<InvalidOperationException>(() => planet.SpendFunds(10.5));
            }

            [Test]
            public void AddWeaponShouldAddWeaponToCollectionIfNotExist()
            {
                var planet = new Planet("Mars", 10);
                var weapon = new Weapon("Nucleaer", 9, 9);
                planet.AddWeapon(weapon);

                Assert.AreEqual(1, planet.Weapons.Count);
                Assert.That(planet.Weapons.Contains(weapon));
            }

            [Test]
            public void AddWeaponShouldThrowExceptionIfAlreadyExists()
            {
                var planet = new Planet("Mars", 10);
                var weapon = new Weapon("Nucleaer", 9, 9);
                planet.AddWeapon(weapon);

                Assert.Throws<InvalidOperationException>(() => planet.AddWeapon(weapon));
            }

            [Test]
            public void RemoveWeaponShouldRemoveWeaponIfExists()
            {
                var planet = new Planet("Mars", 10);
                var weapon = new Weapon("Nucleaer", 9, 9);
                planet.AddWeapon(weapon);

                planet.RemoveWeapon("Nucleaer");
                planet.RemoveWeapon("NoSuchWeapon");

                Assert.AreEqual(0, planet.Weapons.Count);
                Assert.That(!planet.Weapons.Contains(weapon));
            }

            [Test]
            public void UpgradeWeaponShouldIncreaseDestructionLevelIfWeaponExists()
            {
                var planet = new Planet("Mars", 10);
                var weaponName = "Nuclear";
                var weapon = new Weapon(weaponName, 9, 9);
                planet.AddWeapon(weapon);
                planet.UpgradeWeapon(weaponName);

                Assert.AreEqual(10, planet.Weapons.FirstOrDefault(x => x.Name == weaponName).DestructionLevel);
            }

            [Test]
            public void UpgradeWeaponShouldThrowExceptionIfWeaponDoesNotExist()
            {
                var planet = new Planet("Mars", 10);

                Assert.Throws<InvalidOperationException>(() => planet.UpgradeWeapon("Missle"));
            }

            [Test]
            public void DestructOpponentShouldPassIfOpponentPowerRatioIsLower()
            {
                var planet = new Planet("Jupiter", 20);
                planet.AddWeapon(new Weapon("Missle", 10, 8));
                planet.AddWeapon(new Weapon("Other", 5, 4));
                planet.AddWeapon(new Weapon("Light", 1, 1));

                var opponentPlanet = new Planet("Mercury", 20);
                opponentPlanet.AddWeapon(new Weapon("Missle", 10, 8));
                opponentPlanet.AddWeapon(new Weapon("Other", 5, 4));

                var result = planet.DestructOpponent(opponentPlanet);
                var expected = $"{opponentPlanet.Name} is destructed!";

                Assert.AreEqual(expected, result);
            }

            [Test]
            public void DestructOpponentShouldThrowExceptionIfOpponentPowerRatioIsHigher()
            {
                var planet = new Planet("Jupiter", 20);
                planet.AddWeapon(new Weapon("Missle", 10, 8));
                planet.AddWeapon(new Weapon("Other", 5, 4));

                var opponentPlanet = new Planet("Mercury", 20);
                opponentPlanet.AddWeapon(new Weapon("Missle", 10, 8));
                opponentPlanet.AddWeapon(new Weapon("Other", 5, 4));
                opponentPlanet.AddWeapon(new Weapon("Light", 1, 1));

                Assert.Throws<InvalidOperationException>(() => planet.DestructOpponent(opponentPlanet));
            }

            [Test]
            public void MilitaryPowerRatioShouldReturnTheSumOfAllWeaponsDestructionLevels()
            {
                var planet = new Planet("Jupiter", 20);
                planet.AddWeapon(new Weapon("Missle", 10, 8));
                planet.AddWeapon(new Weapon("Other", 5, 4));

                Assert.AreEqual(12, planet.MilitaryPowerRatio);
            }

            [Test]
            public void WeaponConstructorShouldFailWihtInvalidPrice()
            {
                Assert.Throws<ArgumentException>(() => new Weapon("asd", -1, 1)); 
            }

            [Test]
            public void IsNuclearShouldReturnTrueIfDestructionlevelisHigherOrEqualTo10()
            {
                var wepon1 = new Weapon("aa", 10, 9);
                var wepon2 = new Weapon("bb", 10, 10);
                var wepon3 = new Weapon("cc", 10, 11);

                Assert.IsFalse(wepon1.IsNuclear);
                Assert.IsTrue(wepon2.IsNuclear);
                Assert.IsTrue(wepon3.IsNuclear);
            }
        }
    }
}
