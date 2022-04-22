using Moq;
using NUnit.Framework;

namespace AxeAndDummy.Tests
{
    [TestFixture]
    public class HeroTests
    {
        [Test]
        public void Test_HeroShouldGainExperience_WhenAttackedTargetDies()
        {
            Mock<IWeapon> weapon = new Mock<IWeapon>();
            Mock<ITarget> target = new Mock<ITarget>();
            target.Setup(x => x.IsDead()).Returns(true);
            target.Setup(x => x.GiveExperience()).Returns(50);

            Hero hero = new Hero("Hero", weapon.Object);
            hero.Attack(target.Object);

            target.Verify(x => x.GiveExperience());
            Assert.AreEqual(50, hero.Experience);
        }
    }
}
