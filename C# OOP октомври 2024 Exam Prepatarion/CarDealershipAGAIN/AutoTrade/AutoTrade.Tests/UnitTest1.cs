using System;
using System.Linq;
using NUnit.Framework;

namespace AutoTrade.Tests
{
    [TestFixture]
    public class DealerShopTests
    {
        private DealerShop dealerShop;
        private Vehicle vehicle1;
        private Vehicle vehicle2;

        [SetUp]
        public void Setup()
        {
            dealerShop = new DealerShop(2);
            vehicle1 = new Vehicle("Toyota", "Corolla", 2020);
            vehicle2 = new Vehicle("Ford", "Fiesta", 2019);
        }

        [Test]
        public void CtorInitializeeebe()
        {
            Assert.AreEqual(2, dealerShop.Capacity);
            Assert.IsNotNull(dealerShop.Vehicles);
            Assert.AreEqual(0, dealerShop.Vehicles.Count);
        }

        [Test]
        public void Capacity_exc_kogatoValueEpoMalko_ot_1()
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() => new DealerShop(0));
            Assert.AreEqual("Capacity must be a positive value.", exception.Message);
        }

        [Test]
        public void AddVehicle_dobavqVehicleKgatopozvolqva()
        {
            string result = dealerShop.AddVehicle(vehicle1);

            Assert.AreEqual($"Added {vehicle1}", result);
            Assert.AreEqual(1, dealerShop.Vehicles.Count);
            Assert.IsTrue(dealerShop.Vehicles.Contains(vehicle1));
        }

        [Test]
        public void AddVehicle_AddVehicle_hvyrlqExc_kogatoenaCapaciti()
        {
            dealerShop.AddVehicle(vehicle1);
            dealerShop.AddVehicle(vehicle2);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => dealerShop.AddVehicle(new Vehicle("Honda", "Civic", 2021)));
            Assert.AreEqual("Inventory is full", exception.Message);
        }

        [Test]
        public void SellVehicle_mahaVehicleivrushtaTRUE()
        {
            dealerShop.AddVehicle(vehicle1);

            bool result = dealerShop.SellVehicle(vehicle1);

            Assert.IsTrue(result);
            Assert.AreEqual(0, dealerShop.Vehicles.Count);
        }

        [Test]
        public void SellVehicle_vrushtaTRUEkogatoNeSushtestvuva()
        {
            bool result = dealerShop.SellVehicle(vehicle1);

            Assert.IsFalse(result);
        }

        [Test]
        public void Inventorireport()
        {
            dealerShop.AddVehicle(vehicle1);
            dealerShop.AddVehicle(vehicle2);

            string expectedReport = $"Inventory Report{Environment.NewLine}" +
                                    $"Capacity: 2{Environment.NewLine}" +
                                    $"Vehicles: 2{Environment.NewLine}" +
                                    $"{vehicle1}{Environment.NewLine}" +
                                    $"{vehicle2}";

            string actualReport = dealerShop.InventoryReport();

            Assert.AreEqual(expectedReport, actualReport);
        }

        [Test]
        public void InventoriReportPrazen()
        {
            string expectedReport = $"Inventory Report{Environment.NewLine}" +
                                    $"Capacity: 2{Environment.NewLine}" +
                                    $"Vehicles: 0";

            string actualReport = dealerShop.InventoryReport();

            Assert.AreEqual(expectedReport, actualReport);
        }
    }
}
