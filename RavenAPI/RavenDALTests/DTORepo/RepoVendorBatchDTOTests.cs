using Microsoft.VisualStudio.TestTools.UnitTesting;
using RavenDAL.Data;
using RavenDAL.DTORepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDAL.DTORepo.Tests
{
    [TestClass()]
    public class RepoVendorBatchDTOTests
    {
        [TestMethod()]
        public void GetByLotNumberTest()
        {
            RavenDBContext ctx = new RavenDBContext();
            RepoVendorBatchDTO vendor = new RepoVendorBatchDTO(ctx);

            var vendorBatch = vendor.GetByLotNumber("222-761-767");
            Assert.AreEqual(5,vendorBatch.Quantity);
            StringAssert.Contains("RAW31102", vendorBatch.Sample.SampleSubmitNumber);
        }
    }
}