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
    public class VendorBatchTests
    {
        [TestMethod()]
        public void getVendorBatchFromDatabaseTest()
        {
            VendorBatch batch = new VendorBatch();
            var vendorBatchNumber = "123-abc1234";
            var result = batch.getVendorBatch(45235,"Silabond");
            StringAssert.Contains(vendorBatchNumber, result[0].vendorBatchNumber);
        }
    }
}