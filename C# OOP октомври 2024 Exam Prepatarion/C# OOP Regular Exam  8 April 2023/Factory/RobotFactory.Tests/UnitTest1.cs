using NUnit.Framework;
using System;
using System.Text;
using System.Collections.Generic;
using RobotFactory;

namespace RobotFactory.Tests
{
    public class UnitTests1
    {
        private Factory factory;
        private Robot robot;
        private Supplement supplement;

        [SetUp]
        public void Setup()
        {
            factory = new Factory("Robot Factory", 10);
            robot = new Robot("Robo1", 500.0, 101);
            supplement = new Supplement("SpeedBoost", 101);
        }

        [Test]
        public void TestPraiRobotAkoCappacityPozvolqva()
        {
            string expected = "Produced --> Robot model: Robo1 IS: 101, Price: 500.00";
            string actual = factory.ProduceRobot("Robo1", 500.0, 101);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testPraiRobot_NeTrqbvadapraviakonqmacapacity()
        {
            for (int i = 0; i < 10; i++)
            {
                factory.ProduceRobot($"Robo{i}", 500.0, 101);
            }

            string actual = factory.ProduceRobot("RoboOverflow", 500.0, 101);
            string expected = "The factory is unable to produce more robots for this production day!";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestPraiSupplement()
        {
            string expected = "Supplement: SpeedBoost IS: 101";
            string actual = factory.ProduceSupplement("SpeedBoost", 101);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestUpgrRobotaAkoConditionsSaKaktoTrqaa()
        {
            factory.ProduceRobot("Robo1", 500.0, 101);
            factory.ProduceSupplement("SpeedBoost", 101);

            bool result = factory.UpgradeRobot(robot, supplement);

            Assert.IsTrue(result);
            Assert.AreEqual(1, robot.Supplements.Count);
        }

        [Test]
        public void TestUpgrdRbtNeTrqaaDaUpgradevaAkoEvecheupgadenat()
        {
            factory.ProduceRobot("Robo1", 500.0, 101);
            factory.ProduceSupplement("SpeedBoost", 101);

            factory.UpgradeRobot(robot, supplement);
            bool result = factory.UpgradeRobot(robot, supplement); 

            Assert.IsFalse(result);
        }

        [Test]
        public void TestUpgrdnetrqaadaupgradevaakointerfeisaevguzu()
        {
            factory.ProduceRobot("Robo1", 500.0, 101);
            Supplement incompatibleSupplement = new Supplement("PowerBoost", 102);

            bool result = factory.UpgradeRobot(robot, incompatibleSupplement);

            Assert.IsFalse(result);
        }

        [Test]
        public void TestSelRbtTrqaadagovurneAkoCenataEAdeq()
        {
            factory.ProduceRobot("Robo1", 500.0, 101);
            factory.ProduceRobot("Robo2", 300.0, 101);

            Robot result = factory.SellRobot(400.0);

            Assert.AreEqual("Robo2", result.Model);
            Assert.AreEqual(300.0, result.Price);
        }


        [Test]
        public void TestSellRobtrqaadavyrnenullakonqmarobotstakavacena()
        {
            factory.ProduceRobot("Robo1", 500.0, 101);
            factory.ProduceRobot("Robo2", 300.0, 101);

            Robot result = factory.SellRobot(200.0);

            Assert.IsNull(result);
        }
    }
}
