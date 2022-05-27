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
            var vendorBatchNumber = "123ABC4567";
            var result = batch.getVendorBatch(45235,"Silabond");
            StringAssert.Contains(vendorBatchNumber, result[0].vendorBatchNumber);
        }
        [TestMethod()]
        public void getVendorBatchList()
        {
            var vendorBatch = new VendorBatch();
            var batch = vendorBatch.getVendorBatch(45235,"silabond");

            StringAssert.Contains("123ABC4567", batch[0].vendorBatchNumber);
        }

    }
}