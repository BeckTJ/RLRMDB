using Microsoft.VisualStudio.TestTools.UnitTesting;
using RLRMWF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLRMWF.Tests
{
    [TestClass()]
    public class VendorsTests
    {
        [TestMethod()]
        public void getVendorFromDatabaseTest()
        {
            Vendors vendor = new Vendors();
            var result = vendor.getVendorIdFromDatabase("Silabond");
            Assert.AreEqual(5,result);
        }
    }
}