namespace Robots.Tests
{
    using System;
    using NUnit.Framework;

    public class RobotsTests
    {
        [TestCase(10)]
        [TestCase(0)]
        public void RobotManagerConstructorShouldInitializeWithValidData(int capacity)
        {
            RobotManager manager = new RobotManager(capacity);

            Assert.AreEqual(capacity, manager.Capacity);
            Assert.AreEqual(0, manager.Count);
        }

        [Test]
        public void RobotManagerConstructorShouldThrowExceptionWihtNegativeCapacity()
        {
            Assert.Throws<ArgumentException>(() => new RobotManager(-1));
        }

        [Test]
        public void AddShouldAddRobotToCollection()
        {
            RobotManager manager = new RobotManager(10);
            Robot robot = new Robot("Gogo", 100);
            manager.Add(robot);

            Assert.AreEqual(1, manager.Count);
        }

        [Test]
        public void AddShouldThrowExceptionIfRobotIsAlreadyAdded()
        {
            RobotManager manager = new RobotManager(10);
            Robot robot = new Robot("Gogo", 100);
            manager.Add(robot);

            Assert.Throws<InvalidOperationException>(() => manager.Add(robot));
        }

        [Test]
        public void AddShouldThrowExceptionIfCapacityIsFull()
        {
            RobotManager manager = new RobotManager(1);
            Robot robot1 = new Robot("Gogo", 100);
            Robot robot2 = new Robot("Misho", 100);
            manager.Add(robot1);

            Assert.Throws<InvalidOperationException>(() => manager.Add(robot2));
        }

        [Test]
        public void RemoveShouldRemoveRobotFromCollection()
        {
            RobotManager manager = new RobotManager(10);
            Robot robot = new Robot("Gogo", 100);
            manager.Add(robot);
            manager.Remove("Gogo");

            Assert.AreEqual(0, manager.Count);
        }

        [Test]
        public void RemoveShouldThrowExceptionIfRobotDoesNotExist()
        {
            RobotManager manager = new RobotManager(10);
            Robot robot = new Robot("Gogo", 100);
            manager.Add(robot);

            Assert.Throws<InvalidOperationException>(() => manager.Remove("Fo"));
        }

        [Test]
        public void WorkShouldDecreaseRobotBattery()
        {
            RobotManager manager = new RobotManager(10);
            Robot robot = new Robot("Gogo", 100);
            manager.Add(robot);
            manager.Work("Gogo", "clean", 50);

            Assert.AreEqual(50, robot.Battery);
        }

        [Test]
        public void WorkShouldThrowExceptionIfRobotDoesNotExist()
        {
            RobotManager manager = new RobotManager(10);
            Robot robot = new Robot("Gogo", 100);
            manager.Add(robot);

            Assert.Throws<InvalidOperationException>(() => manager.Work("Mu", "clean", 50));
        }

        [Test]
        public void WorkShouldThrowExceptionIfRobotBatteryIsLessThanBatteryUsage()
        {
            RobotManager manager = new RobotManager(1);
            Robot robot = new Robot("Gogo", 100);
            manager.Add(robot);

            Assert.Throws<InvalidOperationException>(() => manager.Work("Gogo", "clean", 150));
        }

        [Test]
        public void ChargeShouldIncreaseRobotBatteryToTheMax()
        {
            RobotManager manager = new RobotManager(1);
            Robot robot = new Robot("Gogo", 100);
            manager.Add(robot);
            manager.Work("Gogo", "clean", 50);
            manager.Charge("Gogo");

            Assert.AreEqual(100, robot.Battery);
        }

        [Test]
        public void ChargeShouldThrowExceptionIfRobotDoesNotExist()
        {
            RobotManager manager = new RobotManager(1);
            Robot robot = new Robot("Gogo", 100);
            manager.Add(robot);

            Assert.Throws<InvalidOperationException>(() => manager.Charge("Mitko"));
        }
    }
}
