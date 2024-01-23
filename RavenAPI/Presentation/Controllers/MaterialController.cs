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
    public async Task<IActionResult> GetAllMaterial()
    {
        var materials = await _service.MaterialServices.GetAllMaterials();

        return Ok(materials);

    }
    [HttpGet("{materialNumber:int}")]
    public async Task<IActionResult> GetMaterialByMaterialNumber(int materialNumber) 
    {
        var material = await _service.MaterialServices.GetMaterialByMaterialNumber(materialNumber);

        return Ok(material);        
    }
}