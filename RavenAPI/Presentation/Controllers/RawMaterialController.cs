using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTO;

namespace Presentation.Controllers;

[Route("RavenAPI/RawMaterial")]
[ApiController]

public class RawMaterialController : ControllerBase
{
    private readonly IServiceManager _service;

    public RawMaterialController( IServiceManager repo)
    {
        _service = repo;
    }

    [HttpGet]
    public IActionResult GetAllRawMaterial()
    {
            var material = _service.RawMaterialService.GetAllRawMaterial();

            return Ok(material);
    }
    [HttpGet("{ParentMaterialNumber:int}", Name = "GetApprovedRawMaterial")]
    public IActionResult GetApprovedRawMaterial(int parentMaterialNumber)
    {
        var rawMaterial = _service.RawMaterialService.GetApprovedRawMaterial(parentMaterialNumber);
        return Ok(rawMaterial);
    }
    [HttpGet("{ParentMaterialNumber:int}", Name = "GetRawMaterialByMaterialNumber")]
    public IActionResult GetRawMaterialByMaterialNumber(int parentMaterialNumber)
    {
        var rawMaterial = _service.RawMaterialService.GetRawMaterialByMaterialNumber(parentMaterialNumber);
        return Ok(rawMaterial);
    }
    [HttpGet("{ProductId:string}", Name = "GetRawMaterialByProductId")]
    public IActionResult GetRawMaterialByProductId(string productId)
    {
        var rawMaterial = _service.RawMaterialService.GetRawMaterialByProductId(productId);
        return Ok(rawMaterial);
    }
    [HttpPost]
    public IActionResult CreateRawMaterial([FromBody]CreateRawMaterialDTO rawMaterial)
    {
        if(rawMaterial == null)
            return BadRequest("Object is null");

        var createdRawMaterial = _service.RawMaterialService.InputRawMaterial(rawMaterial);
        return CreatedAtRoute("GetRawMaterialByMaterialNumber", createdRawMaterial);
    }
}