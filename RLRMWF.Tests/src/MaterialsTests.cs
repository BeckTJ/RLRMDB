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
        [TestMethod()]
        public void getMaterialTest()
        {
            Materials material = new Materials();
            var materialNumber = 45235;
            var result = material.getMaterial(materialNumber);

        }
    }
}