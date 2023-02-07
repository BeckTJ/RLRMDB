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
        var vendor = RawMaterialSRC.getVendor(materialNumber);

        Assert.Equal(4, vendor.Count());
    }
    [Fact]
    public void getMaterialNumber()
    {
        int materialNumber = 58971;
        var materialNumbers = MaterialDTO.getMaterialNumberFromParent(materialNumber);

        Assert.Equal(3, materialNumbers.Count());

    }

}