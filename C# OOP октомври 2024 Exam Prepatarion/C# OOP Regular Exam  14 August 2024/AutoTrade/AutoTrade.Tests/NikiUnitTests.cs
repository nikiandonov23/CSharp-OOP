using System;
using System.Linq;
using NUnit.Framework;

namespace AutoTrade.Tests
{
    [TestFixture]
    public class DealerShopTests
    {
        private DealerShop dealerShop;
        private Vehicle vehicle;

        [SetUp]
        public void Setup()
        {
            dealerShop = new DealerShop(5);
            vehicle = new Vehicle("Toyota", "Corolla", 2020);
        }

        [Test]
        public void Constructor_ShouldInitializeProperly()
        {
            Assert.AreEqual(5, dealerShop.Capacity);
            Assert.IsNotNull(dealerShop.Vehicles);
            Assert.AreEqual(0, dealerShop.Vehicles.Count);
        }

        [Test]
        public void Capacity_ShouldThrowException_IfValueIsLessThanOne()
        {
            Assert.Throws<ArgumentException>(() => new DealerShop(0));
        }

        [Test]
        public void AddVehicle_ShouldAddVehicleToTheInventory()
        {
            string result = dealerShop.AddVehicle(vehicle);

            Assert.AreEqual(1, dealerShop.Vehicles.Count);
            Assert.IsTrue(dealerShop.Vehicles.Contains(vehicle));
            Assert.AreEqual($"Added {vehicle}", result);
        }

        [Test]
        public void AddVehicle_ShouldThrowException_IfInventoryIsFull()
        {
            for (int i = 0; i < dealerShop.Capacity; i++)
            {
                dealerShop.AddVehicle(new Vehicle("Make", "Model", 2000 + i));
            }

            Assert.Throws<InvalidOperationException>(() => dealerShop.AddVehicle(vehicle));
        }

        [Test]
        public void SellVehicle_ShouldRemoveVehicleFromInventory()
        {
            dealerShop.AddVehicle(vehicle);

            bool result = dealerShop.SellVehicle(vehicle);

            Assert.IsTrue(result);
            Assert.AreEqual(0, dealerShop.Vehicles.Count);
        }

        [Test]
        public void SellVehicle_ShouldReturnFalse_IfVehicleDoesNotExist()
        {
            bool result = dealerShop.SellVehicle(vehicle);

            Assert.IsFalse(result);
        }

        [Test]
        public void InventoryReport_ShouldReturnCorrectInformation()
        {
            // Arrange
            dealerShop.AddVehicle(vehicle);
            dealerShop.AddVehicle(new Vehicle("Honda", "Civic", 2019));

            string expectedReport =
                $"Inventory Report{Environment.NewLine}" +
                $"Capacity: 5{Environment.NewLine}" +
                $"Vehicles: 2{Environment.NewLine}" +
                $"2020 Toyota Corolla{Environment.NewLine}" +
                $"2019 Honda Civic";

            // Act
            string actualReport = dealerShop.InventoryReport();

            // Assert
            Assert.AreEqual(expectedReport, actualReport);
        }
    }
}
