using System.Text;
using NUnit.Framework;

namespace SecureOpsSystem.Tests
{
    [TestFixture]
    public class UnitTests1
    {
        [Test]
        public void AtTll_ShAddTll_WhenCpctIsAvl()
        {
            var sh = new SecureHub(3);
            var t = new SecurityTool("T1", "Cat1", 75);

            var r = sh.AddTool(t);

            Assert.AreEqual("Security Tool T1 added successfully.", r);
            Assert.AreEqual(1, sh.Tools.Count);
        }

        [Test]
        public void AtTll_ShRetMsg_WhenTllExst()
        {
            var sh = new SecureHub(3);
            var t1 = new SecurityTool("T1", "Cat1", 75);
            sh.AddTool(t1);

            var t2 = new SecurityTool("T1", "Cat2", 80);

            var r = sh.AddTool(t2);

            Assert.AreEqual("Security Tool T1 already exists in the hub.", r);
            Assert.AreEqual(1, sh.Tools.Count);
            
        }

        [Test]
        public void AtTl_ShRetMsg_WenHubIsFl()
        {
            var sh = new SecureHub(2);
            var t1 = new SecurityTool("T1", "Cat1", 75);
            var t2 = new SecurityTool("T2", "Cat2", 80);
            sh.AddTool(t1);
            sh.AddTool(t2);

            var t3 = new SecurityTool("T3", "Cat3", 85);

            var r = sh.AddTool(t3);

            Assert.AreEqual("Secure Hub is at full capacity.", r);
            Assert.AreEqual(2, sh.Tools.Count);
        }

        [Test]
        public void RmTll_TrueShRet_WhenTllRmvdSucf()
        {
            var sh = new SecureHub(3);
            var t = new SecurityTool("T1", "Cat1", 75);
            sh.AddTool(t);

            var r = sh.RemoveTool(t);

            Assert.IsTrue(r);
            Assert.AreEqual(0, sh.Tools.Count);
        }

        [Test]
        public void RmTll_FalseShRet_WenTllNoExsts()
        {
            var sh = new SecureHub(3);
            var t1 = new SecurityTool("T1", "Cat1", 75);
            var t2 = new SecurityTool("T2", "Cat2", 80);
            sh.AddTool(t1);

            var r = sh.RemoveTool(t2);

            Assert.IsFalse(r);
            Assert.AreEqual(1, sh.Tools.Count);
        }

        [Test]
        public void WenTllAvl_DplTl_ShRetTl()
        {
            var sh = new SecureHub(3);
            var t = new SecurityTool("T1", "Cat1", 75);
            sh.AddTool(t);

            var dtll = sh.DeployTool("T1");

            Assert.IsNotNull(dtll);
            Assert.AreEqual("T1", dtll.Name);
            Assert.AreEqual(0, sh.Tools.Count);
        }

        [Test]
        public void ShRetNull_DplTll_WenTllNoExst()
        {
            var sh = new SecureHub(3);
            var t = new SecurityTool("T1", "Cat1", 75);
            sh.AddTool(t);

            var dtll = sh.DeployTool("T2");

            Assert.IsNull(dtll);
            Assert.AreEqual(1, sh.Tools.Count);
        }

        [Test]
        public void SysRpt_ShRetCrctRptWenTllArePrsnt()
        {
            var sh = new SecureHub(3);
            var t1 = new SecurityTool("T1", "Cat1", 75);
            var t2 = new SecurityTool("T2", "Cat2", 80);
            sh.AddTool(t1);
            sh.AddTool(t2);

            var rpt = sh.SystemReport();

            var expRpt = new StringBuilder();
            expRpt.AppendLine("Secure Hub Report:");
            expRpt.AppendLine("Available Tools: 2");
            expRpt.AppendLine("Name: T2, Category: Cat2, Effectiveness: 80.00");
            expRpt.AppendLine("Name: T1, Category: Cat1, Effectiveness: 75.00");

            Assert.AreEqual(expRpt.ToString().TrimEnd(), rpt);
        }
    }
}
