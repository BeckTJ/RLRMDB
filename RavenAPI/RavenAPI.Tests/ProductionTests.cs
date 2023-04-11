using Xunit;
using RavenAPI.src;
using RavenAPI.DTO;

namespace RavenAPI.Tests;

public class ProductTests
{

    [Fact]
    public void getProductLotNumberTest()
    {
        int number = 58245;

        var current = LotNumber.GetLastMaterialLotNumber(number);

        var lot = LotNumber.GetNextProductLotNumber(number);

        Assert.NotEqual(current, lot);
    }

    [Fact]
    public void firstLotNumber()
    {
        string product;
        int materialnumber = 58143;
        product = LotNumber.GetNextProductLotNumber(materialnumber);

        Assert.Equal("700EB", product);
    }
    [Fact]
    public void receiverTest()
    {
        List<string> product = ProductDTO.GetReceivers(58245);

        Assert.Equal(3, product.Count());
    }
    [Fact]
    public void NewRunTest()
    {
        var rawMaterial = ProductLot.StartNewRun(58423, "Ralphs");

        Assert.Equal(2, rawMaterial.RawMaterial.Count());
    }
}
