using Xunit;
using RavenAPI.src;
using RavenAPI.DTO;

namespace RavenAPI.Tests;

public class RawMaterialTests
{
    [Fact]
    public void getVendorFromDB()
    {
        int materialNumber = 58143;
        var vendor = VendorDTO.getVendorFromParent(materialNumber);

        Assert.Equal(4, vendor.Count());
    }
    [Fact]
    public void getMaterialNumber()
    {
        int materialNumber = 58971;
        var materialNumbers = MaterialDTO.GetMaterialNumberFromParent(materialNumber);

        Assert.Equal(3, materialNumbers.Count());
    }
    [Fact]
    public void checkRMforParent()
    {
        var test = RawMaterialDTO.GetRawMaterialByDrumNumber("100CA1C02");

        Assert.Equal(32144, test.MaterialNumber);
    }
    [Fact]
    public void BatchOrDrum()
    {
        int materialNumber1 = 58245;

        int materialNumber2 = 58423;

        bool test1Answer = true;
        bool test2Answer = false;

        Assert.Equal(test1Answer, SampleDTO.SampleRequired(materialNumber1));
        Assert.Equal(test2Answer, SampleDTO.SampleRequired(materialNumber2));
    }
    /* On selection of a vendor a list of raw material drums
    or vendor lot numbers should be given to the UI depending on
    the sample criteria of the chemicals raw material. If a drum
    is selected the program should add the drum to the product run
    if a vendor lot is select the program should auto generate 
    the drum id and add it to the raw material log and the product run
    */
    [Fact]
    public void RawMaterialSelection()
    {

        var rawMaterialDrum = ProductLot.StartNewRun(58245, "Liquor Store"); // all drum sampled (drum)
        var rawMaterialLot = ProductLot.StartNewRun(58423, "Ralphs"); // 1 drums sampled (lot)

        Assert.Equal(5, rawMaterialDrum.RawMaterial.Count());
        Assert.Equal(2, rawMaterialLot.RawMaterial.Count());
    }

}