using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTO;

namespace Presentation.Controllers;

[Route("RavenAPI/RawMaterial")]
[ApiController]

public class MaterialVendorController : ControllerBase
{
    private readonly IServiceManager _service;

    public MaterialVendorController( IServiceManager repo)
    {
        _service = repo;
    }

    [HttpGet("{parentMaterialNumber:int, vendor:string}")]
    public IActionResult GetMaterialVendor(int parentMaterialNumber, string vendorName)
    {
            var materialVendor = _service.MaterialVendorServices.GetMaterialVendor(parentMaterialNumber, vendorName);

            return Ok(materialVendor);
    }

    public void GetApprovedRawMaterial(int materialNumber) { }
    public void GetExpiredRawMaterial(int materialNumber) { }
    public void GetRawMaterialNeedingSample(int parentMaterialNumber) { }
    public void InputRawMaterial(CreateRawMaterialDTO material) { }

}