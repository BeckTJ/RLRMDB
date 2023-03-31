using Xunit;
using RavenAPI.src;
using RavenAPI.DTO;

namespace RavenAPI.Tests;

public class ProductTests
{

    [Fact]
    public void getProductLotNumberTest2()
    {
        // ProductDTO product = new ProductDTO();
        // product.MaterialNumber = 58245;
        // product.ProductLotNumber = LotNumber.GetNextProductLotNumber((int)product.MaterialNumber);
        // product.ProcessOrder = 9999123456;
        // product.ProductBatchNumber = 9991234;
        // product.ReceiverName = "A";

        // ProductDTO.SetProductLot(product);

        int number = 58245;
        var current = LotNumber.GetProductLotNumber(number);

        var lot = LotNumber.GetNextProductLotNumber(number);

        Assert.NotEqual(current.ProductLotNumber, lot);
    }

    [Fact]
    public void firstLotNumber()
    {
        string product;
        int materialnumber = 58245;
        product = LotNumber.GetFirstLotNumber(materialnumber);

        Assert.Equal("200DB", product);
    }
    [Fact]
    public void receiverTest()
    {
        List<string> product = ProductDTO.GetReceivers(58245);

        Assert.Equal(3, product.Count());
    }
}
