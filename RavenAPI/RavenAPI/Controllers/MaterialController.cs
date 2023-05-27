using Microsoft.AspNetCore.Mvc;
using RavenBAL.DTO;
using RavenDAL.Interface;
using RavenBAL.Services;
using RavenDAL.Models;

namespace RavenAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class MaterialController : ControllerBase
{
    private readonly MaterialServices _materialServices;
    private readonly IRepository<Material> _Material;

    public MaterialController(IRepository<Material> material, MaterialServices materialServices)
    {
        _materialServices = materialServices;
        _Material = material;
    }
    [HttpPost("(AddMaterial)")]
    public async Task<Object> AddMaterial([FromBody] Material material)
    {
        try
        {
            await _materialServices.AddMaterial(material);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    // GET all action
    [HttpGet]
    public ActionResult<List<MaterialDTO>> GetAll() => MaterialServices.GetAll();

    // GET by Id action
    [HttpGet("(Materials)")]
    public ActionResult<MaterialDTO> Get(int id)
    {
        var material = MaterialServices.Get(id);

        if (material == null)
            return NotFound();
        return material;
    }
    [HttpGet("(Vendors)")]
    public ActionResult<List<VendorDTO>> GetVendors(int materialNumber)
    {
        var vendors = MaterialServices.GetVendors(materialNumber);

        if (vendors == null)
            return NotFound();
        return vendors;
    }

    // POST action

    // PUT action

    // DELETE action

}