using NUnit.Framework;
using NetTraderSystem;
using System.Linq;

namespace NetTraderSystem.Tests
{
    [TestFixture]
    public class UnitTests1
    {
        private TradingPlatform platform;
        private Product product;

        [SetUp]
        public void Setup()
        {


            platform = new TradingPlatform(5); 
            product = new Product("Laptop", "Electronics", 1500);
        }

        [Test]
        public void TestDobaviProductUspeh()
        {



            var result = platform.AddProduct(product);

            var productCount = platform.Products.Count();




            Assert.AreEqual("Product Laptop added successfully", result);
            Assert.AreEqual(1, productCount);
        }

        [Test]
        public void TestDobaviNeuspehAkoEPulno()
        {




            platform.AddProduct(product); 
            platform.AddProduct(new Product("Phone", "Electronics", 800)); 
            platform.AddProduct(new Product("Tablet", "Electronics", 600)); 



            platform.AddProduct(new Product("Headphones", "Electronics", 200)); 
            platform.AddProduct(new Product("Watch", "Electronics", 300)); 
            var result = platform.AddProduct(new Product("Camera", "Electronics", 1000)); 

            var productCount = platform.Products.Count();


            Assert.AreEqual("Inventory is full", result); 

            Assert.AreEqual(5, productCount); 
        }

        [Test]
        public void TestIztriiUspeh()
        {
            platform.AddProduct(product); 

            var isRemoved = platform.RemoveProduct(product);

            var productCount = platform.Products.Count();

            Assert.IsTrue(isRemoved);

            Assert.AreEqual(0, productCount); 
        }

        [Test]
        public void TestPriNeuspeh()
        {
            
            platform.AddProduct(product); 



            var newProduct = new Product("Phone", "Electronics", 800); 

            
            var isRemoved = platform.RemoveProduct(newProduct);

            var productCount = platform.Products.Count();

            
            Assert.IsFalse(isRemoved); 




            Assert.AreEqual(1, productCount); 
        }

        [Test]
        public void TestUspehProdai()
        {
            
            platform.AddProduct(product); 

            
            var soldProduct = platform.SellProduct(product);

            var productCount = platform.Products.Count();

            
            Assert.AreEqual(product, soldProduct); 
            Assert.AreEqual(0, productCount); 
        }

        [Test]
        public void TestNeuspehPSel()
        {
            
            platform.AddProduct(product); 
            var newProduct = new Product("Phone", "Electronics", 800); 

            
            var soldProduct = platform.SellProduct(newProduct);



            var productCount = platform.Products.Count();

            
            Assert.IsNull(soldProduct); 


            Assert.AreEqual(1, productCount); 
        }

        [Test]
        public void TestInventar()
        {
            
            platform.AddProduct(product);

            platform.AddProduct(new Product("Phone", "Electronics", 800));

            platform.AddProduct(new Product("Tablet", "Electronics", 600));

            
            var report = platform.InventoryReport();
            StringAssert.Contains("Inventory Report:", report); 

            StringAssert.Contains("Available Products: 3", report); 



            StringAssert.Contains("Name: Laptop, Category: Electronics - $1500.00", report); 
        }
    }
}
