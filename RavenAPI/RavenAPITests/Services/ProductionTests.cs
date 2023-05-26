using Microsoft.VisualStudio.TestTools.UnitTesting;
using RavenAPI.DTO;
using RavenAPI.src;

namespace RavenAPI.Tests;

[TestClass]
public class ProductTests
{

    [TestMethod()]
    public void getProductLotNumberTest()
    {
        int number = 58245;

        var current = LotNumber.GetLastMaterialLotNumber(number);

        var lot = LotNumber.GetNextProductLotNumber(number);

        Equals(current, lot);
    }

    [TestMethod()]
    public void firstLotNumber()
    {
        string product;
        int materialnumber = 58143;
        product = LotNumber.GetNextProductLotNumber(materialnumber);

        StringAssert.Contains("700EB", product);
    }
    [TestMethod()]
    public void receiverTest()
    {
        List<string> product = ProductDTO.GetReceivers(58245);

        Assert.AreEqual(3, product.Count());
    }
}
