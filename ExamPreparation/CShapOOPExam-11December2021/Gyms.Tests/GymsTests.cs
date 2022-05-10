using System;
using NUnit.Framework;

namespace Gyms.Tests
{
    public class GymsTests
    {
        [Test]
        public void Test_AthleteCreation()
        {
            Athlete athlete = new Athlete("John");

            Assert.AreEqual("John", athlete.FullName);
            Assert.AreEqual(false, athlete.IsInjured);
        }

        [TestCase("IronGym", 100)]
        [TestCase("TheGym", 0)]
        public void Test_GymCreation_Positive(string name, int capacity)
        {
            Gym gym = new Gym(name, capacity);

            Assert.AreEqual(name, gym.Name);
            Assert.AreEqual(capacity, gym.Capacity);
            Assert.AreEqual(0, gym.Count);
        }

        [TestCase("", 100)]
        [TestCase(null, 100)]
        public void Test_GymCreation_ShouldThrowException_WithInvalidName(string name, int capacity)
        {
            Assert.Throws<ArgumentNullException>(() => new Gym(name, capacity));
        }

        [TestCase("IronGym", -1)]
        [TestCase("StongBones", int.MinValue)]
        public void Test_GymCreation_ShouldThrowException_WithInvalidCapacity(string name, int capacity)
        {
            Assert.Throws<ArgumentException>(() => new Gym(name, capacity));
        }

        [Test]
        public void Test_AddAthlete_Positive()
        {
            Gym gym = new Gym("Iron", 5);

            gym.AddAthlete(new Athlete("Ivan"));
            Assert.AreEqual(1, gym.Count);

            gym.AddAthlete(new Athlete("Stephen"));
            gym.AddAthlete(new Athlete("Michael"));
            Assert.AreEqual(3, gym.Count);
        }

        [Test]
        public void Test_AddThlete_ShouldThrowException_IfFull()
        {
            Gym gym = new Gym("Iron", 2);

            gym.AddAthlete(new Athlete("Ivan"));
            gym.AddAthlete(new Athlete("Stephen"));

            Assert.Throws<InvalidOperationException>(() => gym.AddAthlete(new Athlete("Michael")));
        }

        [Test]
        public void Test_RemoveAthlete_Positive()
        {
            Gym gym = new Gym("Iron", 5);

            gym.AddAthlete(new Athlete("Ivan"));
            gym.RemoveAthlete("Ivan");
            Assert.AreEqual(0, gym.Count);

            gym.AddAthlete(new Athlete("Ivan"));
            gym.AddAthlete(new Athlete("Stephen"));
            gym.AddAthlete(new Athlete("Michael"));
            gym.RemoveAthlete("Stephen");
            Assert.AreEqual(2, gym.Count);
        }

        [Test]
        public void Test_RemoveAthlete_ShouldThrowException_IfItDoesNotExist()
        {
            Gym gym = new Gym("Iron", 2);

            gym.AddAthlete(new Athlete("Ivan"));
            gym.AddAthlete(new Athlete("Stephen"));

            Assert.Throws<InvalidOperationException>(() => gym.RemoveAthlete("Mr Bean"));
        }

        [TestCase(new[] { "Ivan" }, "Ivan")]
        [TestCase(new[] { "Arnold", "Migos", "Sean", "Timmy" }, "Migos")]
        public void Test_InjureAthlete_Positive(string[] athletes, string athleteToInjure)
        {
            Gym gym = new Gym("SkinnyFreaks", 18);

            Athlete targetAthlete = null;
            for (int i = 0; i < athletes.Length; i++)
            {
                if (athletes[i] == athleteToInjure)
                {
                    targetAthlete = new Athlete(athletes[i]);
                    gym.AddAthlete(targetAthlete);
                    continue;
                }

                gym.AddAthlete(new Athlete(athletes[i]));
            }

            Athlete injuredAthlete = gym.InjureAthlete(targetAthlete.FullName);

            Assert.AreEqual(athleteToInjure, injuredAthlete.FullName);
            Assert.AreEqual(true, injuredAthlete.IsInjured);
            Assert.AreSame(targetAthlete, injuredAthlete);
        }

        [TestCase(new string[0], "Ivan")]
        [TestCase(new[] { "Arnold", "Migos", "Sean", "Timmy" }, "Lady Gaga")]
        public void Test_InjureAthlete_ShouldThrowException_IfItDoesNotExist(string[] athletes, string athleteToInjure)
        {
            Gym gym = new Gym("SkinnyFreaks", 18);

            for (int i = 0; i < athletes.Length; i++)
            {
                gym.AddAthlete(new Athlete(athletes[i]));
            }

            Assert.Throws<InvalidOperationException>(() => gym.InjureAthlete(athleteToInjure));
        }

        [TestCase(
            new[] { "Arnold","Sammy", "Migos", "Sean", "Timmy" }, 
            new[] {"Sammy", "Timmy" },
            "PureGym",
            "Active athletes at PureGym: Arnold, Migos, Sean")]
        [TestCase(
            new string[0], 
            new string[0],
            "PureGym",
            "Active athletes at PureGym: ")]
        public void Test_Report_ShouldReturn_CorrectData(
            string[] athletes, 
            string[] athletesToInjure,
            string gymName,
            string expectedResult)
        {
            Gym gym = new Gym(gymName, 18);
            for (int i = 0; i < athletes.Length; i++)
            {
                gym.AddAthlete(new Athlete(athletes[i]));
            }

            for (int i = 0; i < athletesToInjure.Length; i++)
            {
                gym.InjureAthlete(athletesToInjure[i]);
            }

            string report = gym.Report();
            Assert.AreEqual(expectedResult, report);
        }
    }
}
