using System;
using System.Linq;
using NUnit.Framework;

public class HeroRepositoryTests
{
    private HeroRepository repository;

    [SetUp]
    public void SetUp()
    {
        repository = new HeroRepository();
    }

    [Test]
    public void ConstructrorShouldInitializeHerroesCollection()
    {
        Assert.AreEqual(0, repository.Heroes.Count);
    }

    [Test]
    public void CreateShoudThrowExceptionIfHeroIsNull()
    {
        Hero hero = null;

        Assert.Throws<ArgumentNullException>(() => repository.Create(hero));
    }

    [Test]
    public void CreateShoudThrowExceptionIfHeroWithSameNameALreadyExists()
    {
        Hero hero1 = new Hero("Mike", 5);
        Hero hero2 = new Hero("Mike", 7);
        repository.Create(hero1);

        Assert.Throws<InvalidOperationException>(() => repository.Create(hero2));
    }

    [Test]
    public void CreateShoulAddHeroToCollectionPositiveTest()
    {
        Hero hero1 = new Hero("Mike", 5);
        Hero hero2 = new Hero("John", 7);
        repository.Create(hero1);
        repository.Create(hero2);

        Assert.That(repository.Heroes.Contains(hero1));
        Assert.That(repository.Heroes.Contains(hero2));
        Assert.AreEqual(2, repository.Heroes.Count);
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase(null)]
    public void ReemoveShoudThrowExceptionIfHeroNameIsNullOrWhiteSpace(string name)
    {
        Hero hero1 = new Hero("Mike", 5);
        repository.Create(hero1);

        Assert.Throws<ArgumentNullException>(() => repository.Remove(name));
    }

    [Test]
    public void RemoveShouldRemoveHeroWithGivenName()
    {
        Hero hero1 = new Hero("Mike", 5);
        Hero hero2 = new Hero("John", 7);
        repository.Create(hero1);
        repository.Create(hero2);

        Assert.IsTrue(repository.Remove("Mike"));
        Assert.That(!repository.Heroes.Contains(hero1));
        Assert.AreEqual(1, repository.Heroes.Count);
    }

    [Test]
    public void GetHeroWithHighestLevelShouldReturnAppropriateHero()
    {
        Hero hero1 = new Hero("Mike", 5);
        Hero hero2 = new Hero("John", 7);
        Hero hero3 = new Hero("Oleg", 1);
        Hero hero4 = new Hero("Me", 9);
        Hero hero5 = new Hero("Nobody", 8);
        Hero hero6 = new Hero("ANother Nobody", 4);
        repository.Create(hero1);
        repository.Create(hero2);
        repository.Create(hero3);
        repository.Create(hero4);
        repository.Create(hero5);
        repository.Create(hero6);

        var hero = repository.GetHeroWithHighestLevel();

        Assert.AreEqual(hero4, hero);
        Assert.AreEqual(hero4.Name, hero.Name);
        Assert.AreEqual(9, hero.Level);
    }

    [Test]
    public void GetHeroShouldReturnHeroWithGivenNameOrNull()
    {
        Hero hero1 = new Hero("Mike", 5);
        Hero hero2 = new Hero("John", 7);
        Hero hero3 = new Hero("Oleg", 1);
        Hero hero4 = new Hero("Me", 9);
        Hero hero5 = new Hero("Nobody", 8);
        Hero hero6 = new Hero("ANother Nobody", 4);
        repository.Create(hero1);
        repository.Create(hero2);
        repository.Create(hero3);
        repository.Create(hero4);
        repository.Create(hero5);
        repository.Create(hero6);

        var hero = repository.GetHero("John");
        var nullHero = repository.GetHero("NotExistent");

        Assert.AreEqual(hero2, hero);
        Assert.That(hero != null);

        Assert.AreEqual(null, nullHero);
        Assert.That(nullHero == null);
    }
}