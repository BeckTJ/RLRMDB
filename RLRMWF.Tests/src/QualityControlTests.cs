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
    public class QualityControlTests
    {
        [TestMethod()]
        public void getSampleInfoTest()
        {
            string sampleNumber = "AAA12345";
            QualityControl qualityControl = new QualityControl();
            var test = qualityControl.getSampleInfo(sampleNumber);

            Assert.IsFalse(test.rejected);
            StringAssert.Contains(sampleNumber, test.sampleNumber);
        }
    }
}