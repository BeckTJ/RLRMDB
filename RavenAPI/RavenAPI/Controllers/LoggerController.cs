using Contracts;
using LoggerService;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]

public class LoggerController : Controller
{
    private readonly ILoggerManager _logger;
    
    public LoggerController(ILoggerManager logger)
    {
        _logger = logger;
    }
    [HttpGet]
    public IEnumerable<string> Get()
    {
        _logger.LogInfo("Info");
        _logger.LogDebug("Debug");
        _logger.LogWarning("warning");
        _logger.LogError("Error");

        return new string[] { "value1", "value2" };
    }

}
