using Microsoft.AspNetCore.Mvc;
using RavenAPI.Services;
using RavenAPI.Models;

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
    public ActionResult<List<Material>> GetAll() =>
        MaterialServices.GetAll();

    // GET by Id action
    [HttpGet("(id)")]
    public ActionResult<Material> Get(int id)
    {
        var material = MaterialServices.Get(id);

        if (material == null)
            return NotFound();
        return material;
    }

    // POST action

    // PUT action

    // DELETE action

}