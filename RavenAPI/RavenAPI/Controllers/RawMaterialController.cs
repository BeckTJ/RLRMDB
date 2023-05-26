using Microsoft.AspNetCore.Mvc;
using RavenAPI.DTO;
using RavenAPI.Services;


namespace RavenAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class RawMaterialController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<RawMaterialDTO>> GetAll() => RawMaterialServices.GetAll();

    [HttpGet("(RawMaterialByMaterialNumber)")]
    public ActionResult<List<RawMaterialDTO>> Get(int materialNumber)
    {
        var rawMaterial = RawMaterialServices.Get(materialNumber);

        if (rawMaterial == null)
            return NotFound();
        return rawMaterial;
    }

    [HttpGet("(RawMaterial)")]
    public ActionResult<object> GetRawMaterial(int materialNumber, string vendor)
    {
        var rawMaterial = RawMaterialServices.RawMaterialSelection(materialNumber, vendor);

        if (rawMaterial == null)
            return NotFound();
        return rawMaterial;
    }

    [HttpGet("(VendorBatchNumberForRawMaterialDrumId)")]
    public ActionResult<RawMaterialDTO> GetRawMaterialDrumId(string drumId) => RawMaterialServices.GetRawMaterial(drumId);

}