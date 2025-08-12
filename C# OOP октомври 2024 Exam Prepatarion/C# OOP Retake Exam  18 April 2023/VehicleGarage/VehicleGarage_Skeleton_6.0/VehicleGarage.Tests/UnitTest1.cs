using NUnit.Framework;
using VehicleGarage;

namespace VehicleGarage.Tests
{
    public class UnitTests1
    {
        private Garage grg;
        private Vehicle vhl;

        [SetUp]
        public void Setup()
        {
            grg = new Garage(2);
            vhl = new Vehicle("Toyota", "Corolla", "123ABC");
        }

        [Test]
        public void AddVhl_ShoudAd_WhenHascap()
        {
            bool result = grg.AddVehicle(vhl);

            Assert.IsTrue(result);
            Assert.AreEqual(1, grg.Vehicles.Count);
        }

        [Test]
        public void AddVhl_ShudReturnFalse_WhenFul()
        {
            grg = new Garage(1);

            grg.AddVehicle(vhl);

            Vehicle vhl2 = new Vehicle("Honda", "Civic", "456DEF");
            bool result = grg.AddVehicle(vhl2);

            Assert.IsFalse(result);
            Assert.AreEqual(1, grg.Vehicles.Count);
        }

        [Test]
        public void AdVhl_ShoudReturnFalse_WhenDoplicateReg()
        {
            grg.AddVehicle(vhl);

            Vehicle vhl2 = new Vehicle("Honda", "Civic", "123ABC");
            bool result = grg.AddVehicle(vhl2);

            Assert.IsFalse(result);
        }

        [Test]
        public void ChargeVhl_SoudCharg_WhenLowBatery()
        {
            vhl.BatteryLevel = 50;
            grg.AddVehicle(vhl);

            int vehiclesCharged = grg.ChargeVehicles(60);

            Assert.AreEqual(1, vehiclesCharged);
            Assert.AreEqual(100, vhl.BatteryLevel);
        }

        [Test]
        public void ChargeVhl_ShodNotCharg_WhenGodBatery()
        {
            vhl.BatteryLevel = 80;
            grg.AddVehicle(vhl);

            int vehiclesCharged = grg.ChargeVehicles(60);

            Assert.AreEqual(0, vehiclesCharged);
        }

        [Test]
        public void DriveVhl_ShudDrainBatery_WhenValid()
        {
            grg.AddVehicle(vhl);
            int initialBatteryLevel = vhl.BatteryLevel;

            grg.DriveVehicle("123ABC", 30, false);

            Assert.AreEqual(initialBatteryLevel - 30, vhl.BatteryLevel);
        }

        [Test]
        public void DriveVhl_ShudNotDrive_WhenDemaged()
        {
            vhl.IsDamaged = true;
            grg.AddVehicle(vhl);

            grg.DriveVehicle("123ABC", 30, false);

            Assert.AreEqual(100, vhl.BatteryLevel);
        }

        [Test]
        public void DriveVhl_ShudNotDrive_WhenHighDrain()
        {
            grg.AddVehicle(vhl);

            grg.DriveVehicle("123ABC", 110, false);

            Assert.AreEqual(100, vhl.BatteryLevel);
        }

        [Test]
        public void RepareVhl_ShudRepare_WhenDemaged()
        {
            vhl.IsDamaged = true;
            grg.AddVehicle(vhl);

            string result = grg.RepairVehicles();

            Assert.AreEqual("Vehicles repaired: 1", result);
            Assert.IsFalse(vhl.IsDamaged);
        }

        [Test]
        public void RepareVhl_ShudReturnZero_WhenNoDamage()
        {
            grg.AddVehicle(vhl);

            string result = grg.RepairVehicles();

            Assert.AreEqual("Vehicles repaired: 0", result);
        }
    }
}
