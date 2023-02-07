using Microsoft.AspNetCore.Mvc;
using RavenAPI.Services;
using RavenAPI.DTO;

namespace RavenAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class ProductController : ControllerBase
{
    public ProductController()
    {

    }

    [HttpGet]
    public ActionResult<List<ProductDTO>> GetAll() => ProductServices.GetAll();

    [HttpGet("(LotNumber)")]
    public ActionResult<ProductDTO> GetProductLot(string lotNumber)
    {
        var product = ProductServices.Get(lotNumber);

        if (product == null)
            return NotFound();
        return product;
    }

    [HttpGet("(MaterialNumber)")]
    public ActionResult<ProductDTO> GetNextLotNumber(int materialNumber)
    {
        ProductDTO product = new ProductDTO();
        product = ProductServices.getNextProductLotNumber(materialNumber);

        if (product == null)
            return NotFound();
        return product;

    }
    [HttpGet("(MaterialNumberForVendor)")]
    public ActionResult<List<string>> GetVendor(int materialNumber)
    {
        List<string> vendors = ProductServices.GetVendors(materialNumber);

        if (vendors == null)
            return NotFound();
        return vendors;
    }
    [HttpPost]
    public void InsertProductLot(ProductDTO product)
    {
        ProductServices.InsertProductLot(product);
    }
}