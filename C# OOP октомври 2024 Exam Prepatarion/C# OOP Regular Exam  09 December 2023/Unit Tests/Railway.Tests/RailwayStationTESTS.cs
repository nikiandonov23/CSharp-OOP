namespace Railway.Tests
{
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class Tests
    {
        private RailwayStation station;

        [SetUp]
        public void Setup()
        {
            station = new RailwayStation("Central Station");
        }

        [Test]
        public void Constructor_ShouldInitializeFieldsCorrectly()
        {
            Assert.AreEqual("Central Station", station.Name);
            Assert.IsNotNull(station.ArrivalTrains);
            Assert.IsNotNull(station.DepartureTrains);
        }

        [Test]
        public void NameProperty_ShouldThrowException_WhenNullOrWhitespace()
        {
            Assert.Throws<ArgumentException>(() => new RailwayStation(null));
            Assert.Throws<ArgumentException>(() => new RailwayStation(""));
            Assert.Throws<ArgumentException>(() => new RailwayStation(" "));
        }

        [Test]
        public void NewArrivalOnBoard_ShouldAddTrainToArrivalTrains()
        {
            station.NewArrivalOnBoard("Train 101");
            Assert.AreEqual(1, station.ArrivalTrains.Count);
            Assert.AreEqual("Train 101", station.ArrivalTrains.Peek());
        }

        [Test]
        public void TrainHasArrived_ShouldReturnMessageAndMoveTrainToDepartureTrains()
        {
            station.NewArrivalOnBoard("Train 101");
            var message = station.TrainHasArrived("Train 101");

            Assert.AreEqual("Train 101 is on the platform and will leave in 5 minutes.", message);
            Assert.AreEqual(0, station.ArrivalTrains.Count);
            Assert.AreEqual(1, station.DepartureTrains.Count);
            Assert.AreEqual("Train 101", station.DepartureTrains.Peek());
        }

        [Test]
        public void TrainHasArrived_ShouldReturnMessageIfOtherTrainsAreWaiting()
        {
            station.NewArrivalOnBoard("Train 101");
            station.NewArrivalOnBoard("Train 102");
            var message = station.TrainHasArrived("Train 102");

            Assert.AreEqual("There are other trains to arrive before Train 102.", message);
            Assert.AreEqual(2, station.ArrivalTrains.Count);
        }

        [Test]
        public void TrainHasLeft_ShouldReturnTrueAndRemoveTrainFromDepartureTrains()
        {
            station.NewArrivalOnBoard("Train 101");
            station.TrainHasArrived("Train 101");
            var result = station.TrainHasLeft("Train 101");

            Assert.IsTrue(result);
            Assert.AreEqual(0, station.DepartureTrains.Count);
        }

        [Test]
        public void TrainHasLeft_ShouldReturnFalseIfTrainIsNotFirstInDepartureTrains()
        {
            station.NewArrivalOnBoard("Train 101");
            station.NewArrivalOnBoard("Train 102");
            station.TrainHasArrived("Train 101");
            station.TrainHasArrived("Train 102");

            var result = station.TrainHasLeft("Train 102");

            Assert.IsFalse(result);
            Assert.AreEqual(2, station.DepartureTrains.Count);
        }

        [Test]
        public void TrainHasLeft_ShouldReturnFalseIfTrainDoesNotExist()
        {
            station.NewArrivalOnBoard("Train 101");
            station.TrainHasArrived("Train 101");

            var result = station.TrainHasLeft("Nonexistent Train");

            Assert.IsFalse(result);
            Assert.AreEqual(1, station.DepartureTrains.Count); // Уверяваме се, че съществуващият влак не е премахнат.
        }
    }
}
