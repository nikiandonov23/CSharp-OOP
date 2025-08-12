global using NUnit.Framework;

namespace Television.Tests
{
    using System;

    public class Tests
    {
        private TelevisionDevice tv;

        [SetUp]
        public void Setup()
        {
            tv = new TelevisionDevice("Sony", 999.99, 1920, 1080);
        }

        [Test]
        public void Ctor_Sazdavam()
        {
            Assert.AreEqual("Sony", tv.Brand);
            Assert.AreEqual(999.99, tv.Price);
            Assert.AreEqual(1920, tv.ScreenWidth);
            Assert.AreEqual(1080, tv.ScreenHeigth);
        }

        [Test]
        public void CurrentChannelvrushtaLast()
        {
            tv.ChangeChannel(10);
            Assert.AreEqual(10, tv.CurrentChannel);
        }

        [Test]
        public void ChangeChannel_AkoENegativExpc()
        {
            Assert.Throws<ArgumentException>(() => tv.ChangeChannel(-1));
        }

        [Test]
        public void ChangeChannel_setvaCorectno()
        {
            tv.ChangeChannel(5);
            Assert.AreEqual(5, tv.CurrentChannel);
        }

        [Test]
        public void VolumeChange_Zvuk_drctUp()
        {
            tv.VolumeChange("UP", 10);
            Assert.AreEqual(23, tv.Volume); // Default volume is 13, adding 10 should make it 23
        }

        [Test]
        public void VolumeChange_ZvukDrktDown()
        {
            tv.VolumeChange("DOWN", 5);
            Assert.AreEqual(8, tv.Volume); // Default volume is 13, subtracting 5 should make it 8
        }

        [Test]
        public void VolumeChange_ZvukDaneprevishavaMAx()
        {
            tv.VolumeChange("UP", 200);
            Assert.AreEqual(100, tv.Volume);
        }

        [Test]
        public void VolumeChange_ZvukDaneprevishavaMin()
        {
            tv.VolumeChange("DOWN", 50);
            Assert.AreEqual(0, tv.Volume);
        }

        [Test]
        public void MuteDevice_toggelvam()
        {
            tv.MuteDevice(); // First mute
            Assert.IsTrue(tv.IsMuted);

            tv.MuteDevice(); // Unmute
            Assert.IsFalse(tv.IsMuted);
        }

        [Test]
        public void SwitchOn_vrushtaSustoqnie1()
        {
            string result = tv.SwitchOn();
            Assert.AreEqual("Cahnnel 0 - Volume 13 - Sound On", result);
        }

        [Test]
        public void SwitchOn_vrushtaSustoqnie2()
        {
            tv.MuteDevice();
            string result = tv.SwitchOn();
            Assert.AreEqual("Cahnnel 0 - Volume 13 - Sound Off", result);
        }

        [Test]
        public void ToString_ba4kaLiIliNe()
        {
            string result = tv.ToString();
            Assert.AreEqual("TV Device: Sony, Screen Resolution: 1920x1080, Price 999.99$", result);
        }
    }
}
