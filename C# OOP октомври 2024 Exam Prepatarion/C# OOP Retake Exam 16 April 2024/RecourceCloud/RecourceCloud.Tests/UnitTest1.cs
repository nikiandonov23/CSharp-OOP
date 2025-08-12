using NUnit.Framework;
using System;
using System.Linq;

namespace RecourceCloud.Tests
{
    public class Tests
    {
        private DepartmentCloud departmentCloud;

        [SetUp]
        public void Setup()
        {
            departmentCloud = new DepartmentCloud();
        }

        [Test]
        public void LogTaskHvurlqExKogatoNeSa3()
        {
            string[] args = new string[] { "1", "Label" };

            Assert.Throws<ArgumentException>(() => departmentCloud.LogTask(args), "All arguments are required.");
        }

        [Test]
        public void LogTaskHvurlqKogatoENULL()
        {
            string[] args = new string[] { "1", "Label", null };

            Assert.Throws<ArgumentException>(() => departmentCloud.LogTask(args), "Arguments values cannot be null.");
        }

        [Test]
        public void LogTaskVrushtaKatoElogged()
        {
            string[] args = new string[] { "1", "Label", "Resource1" };

            departmentCloud.LogTask(args);
            string result = departmentCloud.LogTask(args);

            Assert.AreEqual("Resource1 is already logged.", result);
        }

        [Test]
        public void LogTaskDobavqIVrushtaMsg()
        {
            string[] args = new string[] { "1", "Label", "Resource1" };

            string result = departmentCloud.LogTask(args);

            Assert.AreEqual("Task logged successfully.", result);
            Assert.AreEqual(1, departmentCloud.Tasks.Count);
        }

        [Test]
        public void CreateResourceVrushtaFAlseKatoNeSushtestvuva()
        {
            bool result = departmentCloud.CreateResource();

            Assert.IsFalse(result);
            Assert.AreEqual(0, departmentCloud.Resources.Count);
        }

        [Test]
        public void CreateResourcePraiResSLOwPrioritiIVrushta()
        {
            departmentCloud.LogTask(new string[] { "3", "Label1", "Resource1" });
            departmentCloud.LogTask(new string[] { "1", "Label2", "Resource2" });
            departmentCloud.LogTask(new string[] { "2", "Label3", "Resource3" });

            bool result = departmentCloud.CreateResource();

            Assert.IsTrue(result);
            Assert.AreEqual(1, departmentCloud.Resources.Count);
            Assert.AreEqual("Resource2", departmentCloud.Resources.First().Name);
            Assert.AreEqual("Label2", departmentCloud.Resources.First().ResourceType);
            Assert.AreEqual(2, departmentCloud.Tasks.Count);
        }

        [Test]
        public void TestResourceNullAkoNeSushtestvuva()
        {
            var result = departmentCloud.TestResource("NonExistentResource");

            Assert.IsNull(result);
        }

        [Test]
        public void TestResourceMarkiraGoTEstvanIVrushta()
        {
            departmentCloud.LogTask(new string[] { "1", "Label", "Resource1" });
            departmentCloud.CreateResource();

            var result = departmentCloud.TestResource("Resource1");

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsTested);
            Assert.AreEqual("Resource1", result.Name);
        }

        [Test]
        public void TasksVrushtaReaOnlyyyy()
        {
            var tasks = departmentCloud.Tasks;

            Assert.IsInstanceOf(typeof(System.Collections.ObjectModel.ReadOnlyCollection<Task>), tasks);
        }

        [Test]
        public void ResourcesVrushtaReadOnlyBEeee()
        {
            var resources = departmentCloud.Resources;

            Assert.IsInstanceOf(typeof(System.Collections.ObjectModel.ReadOnlyCollection<Resource>), resources);
        }
    }
}
