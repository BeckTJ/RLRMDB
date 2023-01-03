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


    [HttpGet("(RunLog)")]
    public ActionResult<List<List<string>>> GetRunLog(int materialNumber)
    {
        var runLog = RunLogServices.GetRunLog(materialNumber);

        if (runLog == null)
            return NotFound();
        return runLog;
    }
    [HttpGet("(HourlyRead)")]
    public ActionResult<List<string>> Get(int materialNumber)
    {
        var runLog = RunLogServices.GetHourlyRead(materialNumber);

        if (runLog == null)
            return NotFound();
        return runLog;
    }
    [HttpGet("(PreStart)")]
    public ActionResult<List<string>> GetPreStart(int materialNumber)
    {
        var runLog = RunLogServices.GetPreStart(materialNumber);

        if (runLog == null)
            return NotFound();
        return runLog;
    }
}