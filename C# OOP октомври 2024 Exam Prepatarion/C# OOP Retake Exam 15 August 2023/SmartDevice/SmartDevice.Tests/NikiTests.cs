namespace SmartDevice.Tests
{
    using NUnit.Framework;
    using System;

    public class NikiTests
    {
        private Device device;

        [SetUp]
        public void Setup()
        {
            // Inicializaciq predi vseki test
            device = new Device(100); // Създаваме устройство с 100 MB капацитет
        }

        [Test]
        public void ConstructoraDaBa4ka()
        {
            Assert.AreEqual(100, device.MemoryCapacity);
            Assert.AreEqual(100, device.AvailableMemory);
            Assert.AreEqual(0, device.Photos);
            Assert.IsNotNull(device.Applications);
            Assert.AreEqual(0, device.Applications.Count);
        }

        [Test]
        public void PamettaKatoEDostatuchno()
        {
            bool result = device.TakePhoto(20);

            Assert.IsTrue(result);
            Assert.AreEqual(80, device.AvailableMemory);
            Assert.AreEqual(1, device.Photos);
        }

        [Test]
        public void PamettaKatoNeEDostatuchno()
        {
            bool result = device.TakePhoto(120);

            Assert.IsFalse(result);
            Assert.AreEqual(100, device.AvailableMemory);
            Assert.AreEqual(0, device.Photos);
        }

        [Test]
        public void InstalaciqDobavqnePamettaEDostatuchno()
        {
            string response = device.InstallApp("Facebook", 30);

            Assert.AreEqual("Facebook is installed successfully. Run application?", response);
            Assert.AreEqual(70, device.AvailableMemory);
            Assert.AreEqual(1, device.Applications.Count);
            Assert.AreEqual("Facebook", device.Applications[0]);
        }

        [Test]
        public void InstalaciqDobavqnePamettaNeEDostatuchno()
        {
            Assert.Throws<InvalidOperationException>(() => device.InstallApp("BigApp", 120));

            Assert.AreEqual(100, device.AvailableMemory);
            Assert.AreEqual(0, device.Applications.Count);
        }

        [Test]
        public void FormatiraiBeeeeeee()
        {
            device.TakePhoto(10);
            device.InstallApp("Facebook", 30);

            device.FormatDevice();

            Assert.AreEqual(100, device.AvailableMemory);
            Assert.AreEqual(0, device.Photos);
            Assert.AreEqual(0, device.Applications.Count);
        }


        [Test]
        public void VrushtaLiKorektenStatusNaDevaisa()
        {
            device.TakePhoto(10);
            device.InstallApp("Facebook", 30);

            string status = device.GetDeviceStatus();

            string expectedStatus = "Memory Capacity: 100 MB, Available Memory: 60 MB\r\nPhotos Count: 1\r\nApplications Installed: Facebook";

            Assert.AreEqual(expectedStatus, status);
        }


    }
}
