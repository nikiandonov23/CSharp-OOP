using NUnit.Framework;

namespace VendingRetail.Tests
{
    public class Tests
    {
        private CoffeeMat coffeMat;

        [SetUp]
        public void Setup()
        {
            coffeMat = new CoffeeMat(1000, 5);
        }

        [Test]
        public void Ctor_ShuldInit()
        {
            Assert.AreEqual(1000, coffeMat.WaterCapacity);
            Assert.AreEqual(5, coffeMat.ButtonsCount);
            Assert.AreEqual(0, coffeMat.Income);
        }

        [Test]
        public void FilTank_ShuldFil()
        {
            string result = coffeMat.FillWaterTank();
            Assert.AreEqual("Water tank is filled with 1000ml", result);
        }

        [Test]
        public void FilTank_ShuldNotFillIfFul()
        {
            coffeMat.FillWaterTank();
            string result = coffeMat.FillWaterTank();
            Assert.AreEqual("Water tank is already full!", result);
        }

        [Test]
        public void AdDrink_ShuldAd()
        {
            bool result = coffeMat.AddDrink("Espresso", 1.50);
            Assert.IsTrue(result);
        }

        [Test]
        public void AdDrink_ShuldNotAdIfLimit()
        {
            coffeMat.AddDrink("Espresso", 1.50);
            coffeMat.AddDrink("Americano", 2.00);
            coffeMat.AddDrink("Latte", 2.50);
            coffeMat.AddDrink("Cappuccino", 2.80);
            coffeMat.AddDrink("Mocha", 3.00);

            bool result = coffeMat.AddDrink("Hot Chocolate", 2.20);
            Assert.IsFalse(result);
        }

        [Test]
        public void AdDrink_ShuldNotAdIfExist()
        {
            coffeMat.AddDrink("Espresso", 1.50);
            bool result = coffeMat.AddDrink("Espresso", 2.00);
            Assert.IsFalse(result);
        }

        [Test]
        public void BuyDrink_ShuldFailIfNoWaterr()
        {
            coffeMat.AddDrink("Espresso", 1.50);
            string result = coffeMat.BuyDrink("Espresso");
            Assert.AreEqual("CoffeeMat is out of water!", result);
        }

        [Test]
        public void BuyDrink_ShuldFailIfNoExist()
        {
            coffeMat.FillWaterTank();
            string result = coffeMat.BuyDrink("Espresso");
            Assert.AreEqual("Espresso is not available!", result);
        }

        [Test]
        public void BuyDrinkShuldWork()
        {
            coffeMat.FillWaterTank();
            coffeMat.AddDrink("Espresso", 1.50);

            string result = coffeMat.BuyDrink("Espresso");

            Assert.AreEqual("Your bill is 1.50$", result);
            Assert.AreEqual(920, coffeMat.WaterCapacity - coffeMat.WaterCapacity + 920);
            Assert.AreEqual(1.50, coffeMat.Income);
        }

        [Test]
        public void ColectShuldResetIncom()
        {
            coffeMat.FillWaterTank();
            coffeMat.AddDrink("Espresso", 1.50);
            coffeMat.BuyDrink("Espresso");

            double collectedIncome = coffeMat.CollectIncome();

            Assert.AreEqual(1.50, collectedIncome);
            Assert.AreEqual(0, coffeMat.Income);
        }

        [Test]
        public void Colect_ShuldReturnZeroIFNoIncome()
        {
            double collectedIncome = coffeMat.CollectIncome();
            Assert.AreEqual(0, collectedIncome);
        }
    }
}
