using Microsoft.AspNetCore.Mvc;
using RavenAPI.Services;
using RavenAPI.DTO;

namespace RavenAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class MaterialDistillationController : ControllerBase
{
    public MaterialDistillationController()
    {

    }

    [HttpGet]
    public ActionResult<List<MaterialDistillationDTO>> GetAll() => MaterialDistillationServices.GetAll();

    [HttpGet("(id)")]
    public ActionResult<MaterialDistillationDTO> Get(int materialNumber)
    {
        var preStart = MaterialDistillationServices.Get(materialNumber);
        if (preStart == null)
            return NotFound();
        return preStart;
    }
}