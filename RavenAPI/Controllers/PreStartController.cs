using Microsoft.AspNetCore.Mvc;
using RavenAPI.Services;
using RavenAPI.DTO;

namespace RavenAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PreStartController : ControllerBase
{
    public PreStartController()
    {

    }

    [HttpGet]
    public ActionResult<List<PreStartDTO>> GetAll() => PreStartServices.GetAll();

    [HttpGet("(id)")]
    public ActionResult<PreStartDTO> Get(int materialNumber)
    {
        var preStart = PreStartServices.Get(materialNumber);
        if (preStart == null)
            return NotFound();
        return preStart;
    }
}