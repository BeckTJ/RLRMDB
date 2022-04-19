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
    public class MaterialsTests
    {
        Materials material = new Materials();

        [TestMethod()]
        public void getMaterialTest()
        {
            var materialNumber = 45235;
            var result = material.getMaterial(materialNumber);

            Assert.AreEqual(materialNumber, result.number);
        }
        [TestMethod()]
        public void materialNameId()
        {
            var materialNumber = 45234;
            var result = material.getMaterialNameId(materialNumber);

            Assert.AreEqual(9, result);
        }
    }
}