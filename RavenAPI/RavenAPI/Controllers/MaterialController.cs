using Microsoft.AspNetCore.Mvc;
using RavenAPI.DTO;
using RavenAPI.Services;

namespace RavenAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class MaterialController : ControllerBase
{

    public MaterialController()
    {

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