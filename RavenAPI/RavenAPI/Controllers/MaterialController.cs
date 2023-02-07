using Microsoft.AspNetCore.Mvc;
using RavenAPI.Services;
using RavenAPI.Models;
using RavenAPI.DTO;

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
    [HttpGet("(id)")]
    public ActionResult<MaterialDTO> Get(int id)
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