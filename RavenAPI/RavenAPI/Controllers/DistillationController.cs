using Microsoft.AspNetCore.Mvc;
using RavenAPI.DTO;
using RavenAPI.Services;

namespace RavenAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class DistillationController : ControllerBase
{
    [HttpGet("(AllProductLots)")]
    public ActionResult<List<ProductDTO>> GetAllProductLots() => ProductServices.GetAll();
    
    [HttpGet("(AllDistillationSystems)")]
    public ActionResult<List<DistillationSystemDTO>> GetAllSystems() => DistillationServices.GetAll();

    [HttpGet("(DistillationSystemByMaterialNumber)")]
    public ActionResult<DistillationSystemDTO> GetDistillationSystem(int materialNumber)
    {
        var distillationSystem = DistillationServices.StartDistillation(materialNumber);

        if (distillationSystem == null)
            return NotFound();
        return distillationSystem;
    }
    [HttpGet("(NewProductLot)")]
    public ActionResult<ProductDTO> NewLot(int materialNumber) => ProductServices.StartNewLot(materialNumber);

    [HttpPost("(Update Product Lot)")]
    public void InsertProductLot(ProductDTO product)
    {
        ProductServices.InsertProductLot(product);
    }
}