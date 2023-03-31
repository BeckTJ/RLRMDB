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
        VendorDTO vendor1 = new VendorDTO();
        vendor1.MaterialNumber = 58245;

        VendorDTO vendor2 = new VendorDTO();
        vendor2.MaterialNumber = 58423;

        bool test1Answer = true;
        bool test2Answer = false;

        Assert.Equal(test1Answer, SampleDTO.SampleRequired(vendor1));
        Assert.Equal(test2Answer, SampleDTO.SampleRequired(vendor2));
    }
}