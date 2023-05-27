using Microsoft.VisualStudio.TestTools.UnitTesting;
using RavenDAL.Models;
using RavenDAL.Data;
using RavenBAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenAPI.Services.Tests
{
    [TestClass()]
    public class DistillationServicesTests
    {
        [TestMethod()]
        public void StartDistillationTest()
        {
            RavenDBContext ctx = new RavenDBContext();
            var materialNumber = ctx.Materials.Select(m => m.MaterialNumber).First();
            var distillation = DistillationServices.StartDistillation(materialNumber);

            Assert.AreEqual(materialNumber,distillation.MaterialNumber);
            Assert.AreEqual(3,distillation.ReceiverNames.Count());
        }
    }
}