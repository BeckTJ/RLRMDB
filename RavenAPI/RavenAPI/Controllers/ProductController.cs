using Microsoft.AspNetCore.Mvc;
using RavenAPI.Services;
using RavenAPI.DTO;
using RavenAPI.src;

namespace RavenAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class ProductController : ControllerBase
{
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

    [HttpGet("(Selection)")]
    public ActionResult<ProductLot> ProductSelection(int materialNumber)
    {
        ProductLot product;
        product = ProductServices.ProductSelection(materialNumber);

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

    [HttpPost("(Update Product Lot)")]
    public void InsertProductLot(ProductDTO product)
    {
        ProductServices.InsertProductLot(product);
    }
}