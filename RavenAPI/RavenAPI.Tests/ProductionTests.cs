using Xunit;
using RavenAPI.src;
using RavenAPI.DTO;

namespace RavenAPI.Tests;

public class ProductTests
{

    [Fact]
    public void getProductLotNumberTest2()
    {
        int number = 58143;

        var lot = Product.getNextProductLotNumber(number);

        Assert.Equal("700EB", lot.ProductLotNumber);
    }

    [Fact]
    public void firstLotNumber()
    {
        ProductDTO product = new ProductDTO();
        int materialnumber = 58245;
        product = Product.getFirstLotNumber(materialnumber);

        Assert.Equal("100DB", product.ProductLotNumber);
    }
    [Fact]
    public void InsertTest()
    {
        int materialNumber = 58245;
        ProductDTO product = new ProductDTO();
        List<ProductDTO> testProduct = Product.getProductLot(materialNumber);

        long? processorder = testProduct[0].ProcessOrder + 1;
        int? batch = testProduct[0].ProductBatchNumber + 1;

        product.ProductLotNumber = Product.getNextProductLotNumber(materialNumber).ProductLotNumber;
        product.ProcessOrder = processorder;
        product.ProductBatchNumber = batch;
        product.MaterialNumber = materialNumber;
        product.ReceiverId = 1;

        Product.setProductLot(product);

        ProductDTO newProduct = new ProductDTO();
        newProduct = Product.getProductLotNumber(materialNumber);

        Assert.Equal(58245, newProduct.MaterialNumber);

    }
}
