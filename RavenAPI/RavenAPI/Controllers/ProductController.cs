using Microsoft.AspNetCore.Mvc;
using RavenAPI.Services;
using RavenAPI.DTO;
using RavenAPI.src;

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
    [HttpGet("(RawMaterial)")]
    public ActionResult<List<string>> GetRawMaterial(int materialNumber, string vendor)
    {
        ProductLot product = ProductServices.NewRun(materialNumber, vendor);

        if (product == null)
            return NotFound();
        return product.RawMaterial;
    }

    [HttpPost("(Update Product Lot)")]
    public void InsertProductLot(ProductDTO product)
    {
        ProductServices.InsertProductLot(product);
    }
}