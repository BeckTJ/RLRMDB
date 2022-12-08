using Microsoft.AspNetCore.Mvc;
using RavenAPI.Services;
using RavenAPI.DTO;

namespace RavenAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class RunLogController : ControllerBase
{
    public RunLogController()
    {

    }

    [HttpGet]
    public ActionResult<List<RunLogDTO>> GetAll() => RunLogServices.GetAll();

    [HttpGet("(id)")]
    public ActionResult<List<RunLogDTO>> Get(int materialNumber)
    {
        var runLog = RunLogServices.Get(materialNumber);

        if (runLog == null)
            return NotFound();
        return runLog;
    }
}