using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace Presentation.Controllers;

[Route("RavenAPI/Sample")]
[ApiController]


public class SampleController : ControllerBase
{
    private readonly IServiceManager _repo;

    public SampleController(IServiceManager repo)
    {
        _repo = repo;
    }
    [HttpGet("{parentMaterialNumber:int}")]
    public IActionResult GetSample(int parentMaterialNumber)
    {
        var sample = _repo.SamplingServices.VerifySampleRequired(parentMaterialNumber);
        return Ok(sample);
    }
}