using Microsoft.AspNetCore.Mvc;
using RavenDAL.DTO;
using RavenBAL.Repository;

namespace RavenAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class DistillationController : ControllerBase
{
    [HttpGet("(AllProductLots)")]
    //public ActionResult<List<ProductDTO>> GetAllProductLots() => RepoProduct.GetAll();
    
    [HttpGet("(AllDistillationSystems)")]
    //public ActionResult<List<DistillationSystemDTO>> GetAllSystems() => RepoDistillation.GetAll();
    //
    //[HttpGet("(DistillationSystemByMaterialNumber)")]
    //public ActionResult<DistillationSystemDTO> GetDistillationSystem(int materialNumber)
    //{
    //    var distillationSystem = RepoDistillation.StartDistillation(materialNumber);
    //
    //    if (distillationSystem == null)
    //        return NotFound();
    //    return distillationSystem;
    //}
    [HttpGet("(NewProductLot)")]
    //public ActionResult<ProductDTO> NewLot(int materialNumber) => RepoProduct.StartNewLot(materialNumber);

    [HttpPost("(Update Product Lot)")]
    public void InsertProductLot(ProductDTO product)
    {
     //   RepoProduct.InsertProductLot(product);
    }
}