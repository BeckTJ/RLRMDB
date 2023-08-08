using Microsoft.AspNetCore.Mvc;
using RavenDAL.DTO;
using RavenDAL.Interface;
using RavenDAL.Models;
using RavenBAL.Repository;

namespace RavenAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class MaterialController : ControllerBase
{
    private readonly IMaterialData<MaterialDataDTO> _Material;

    public MaterialController(IMaterialData<MaterialDataDTO> material)
    {
        _Material = material;
    }
    //[HttpPost("(AddMaterial)")]
    //public async Task<Object> AddMaterial([FromBody] Material material)
    //{
    //    try
    //    {
    //        await _materialServices.AddMaterial(material);
    //        return true;
    //    }
    //    catch (Exception ex)
    //    {
    //        return false;
    //    }
    //}
    [HttpGet("Parent")]
    public ActionResult<List<MaterialDataDTO>> GetMaterialNumberFromParent(int materialNumber)
    {
        var material = _Material.GetMaterialNumberFromParent(materialNumber);

        if (material == null)
            return NotFound();
        return material.ToList();

    }

    // GET all action
   //[HttpGet]
   //public ActionResult<List<MaterialDTO>> GetAll() => MaterialServices.GetAll();
   //
   //// GET by Id action
   //[HttpGet("(Materials)")]
   //public ActionResult<MaterialDTO> Get(int id)
   //{
   //    var material = MaterialServices.Get(id);
   //
   //    if (material == null)
   //        return NotFound();
   //    return material;
   //}
   //[HttpGet("(Vendors)")]
   //public ActionResult<List<VendorDTO>> GetVendors(int materialNumber)
   //{
   //    var vendors = MaterialServices.GetVendors(materialNumber);
   //
   //    if (vendors == null)
   //        return NotFound();
   //    return vendors;
   //}

    // POST action

    // PUT action

    // DELETE action

}