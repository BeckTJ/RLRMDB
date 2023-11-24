using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace Presentation.Controllers;

[Route("RavenAPI/Material")]
[ApiController]

public class MaterialController : ControllerBase
{
    private readonly IServiceManager _service;
    public MaterialController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAllMaterial()
    {
        var materials = _service.MaterialServices.GetAllMaterials();

        return Ok(materials);

    }
    [HttpGet("{materialNumber:int}")]
    public IActionResult GetMaterialByMaterialNumber(int materialNumber) 
    {
        var material = _service.MaterialServices.GetMaterialByMaterialNumber(materialNumber);

        return Ok(material);        
    }
}