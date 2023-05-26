using Microsoft.VisualStudio.TestTools.UnitTesting;
using RavenAPI.DTO;
using RavenAPI.Services;
using RavenAPI.src;

namespace RavenAPI.Tests;

[TestClass]
public class RawMaterialTests
{
    [TestMethod()]
    public void GetVendorFromDB()
    {
        int materialNumber = 58143;
        var vendor = VendorDTO.getVendorFromParent(materialNumber);

        Assert.AreEqual(4, vendor.Count);
    }
    [TestMethod()]
    public void getMaterialNumber()
    {
        int materialNumber = 58971;
        var materialNumbers = MaterialDTO.GetMaterialNumberFromParent(materialNumber);

        Assert.AreEqual(3, materialNumbers.Count);
    }
    [TestMethod()]
    public void CheckRMforParent()
    {
        var test = RawMaterialDTO.GetRawMaterialByDrumNumber("100CA1C02");

        Assert.AreEqual(32144, test.MaterialNumber);
    }
    [TestMethod()]
    public void BatchOrDrum()
    {
        int materialNumber1 = 58245;

        int materialNumber2 = 58423;

        bool test1Answer = true;
        bool test2Answer = false;

        Assert.AreEqual(test1Answer, SampleDTO.SampleRequired(materialNumber1));
        Assert.AreEqual(test2Answer, SampleDTO.SampleRequired(materialNumber2));
    }
    [TestMethod()]
    public static void GetRawMaterialFromService()
    {
        int drumMaterialNumber = 32716;
        string drumVendor = "Liquor Store";
        string drumVendorNumber = "222-761-767";
        string drumId = "700DA1C01";

        int vendorMaterialNumber = 3322187;
        string vendor = "Ralphs";
        string vendorBatch = "780-531-555";

        var drum = (List<RawMaterialDTO>)RawMaterialServices.RawMaterialSelection(drumMaterialNumber, drumVendor);
        var materialVendor = (List<VendorDTO>)RawMaterialServices.RawMaterialSelection(vendorMaterialNumber, vendor);
        var id = LotNumber.GetNextProductLotNumber(3322187);

        Assert.AreEqual(5, drum.Count);
        //Assert.Equal(drumId, drum[0].DrumLotNumber);
        StringAssert.Contains(drumVendorNumber, drum[0].Vendor.VendorBatchNumber);

        Assert.AreEqual(2, materialVendor.Count);
        StringAssert.Contains(vendorBatch, materialVendor[0].VendorBatchNumber);
    }
}