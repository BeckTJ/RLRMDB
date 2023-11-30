using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace Presentation.Controllers;

[Route("RavenAPI/RawMaterial")]
[ApiController]

public class RawMaterialController : ControllerBase
{
    private readonly IServiceManager _repo;

    public RawMaterialController( IServiceManager repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public IActionResult GetAllRawMaterial()
    {
            var material = _repo.RawMaterialService.GetAllRawMaterial();

            return Ok(material);
    }
    [HttpGet("{ParentMaterialNumber:int}")]
    public IActionResult GetApprovedRawMaterial(int parentMaterialNumber)
    {
        var rawMaterial = _repo.RawMaterialService.GetApprovedRawMaterial(parentMaterialNumber);
        return Ok(rawMaterial);
    }
}