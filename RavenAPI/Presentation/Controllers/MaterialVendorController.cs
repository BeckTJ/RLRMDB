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
            var materialVendor = _service.VendorServices.GetMaterialVendor(parentMaterialNumber, vendorName);

            return Ok(materialVendor);
    }
}