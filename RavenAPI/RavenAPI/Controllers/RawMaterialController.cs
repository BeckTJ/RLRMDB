using Microsoft.AspNetCore.Mvc;
using RavenAPI.Services;
using RavenAPI.DTO;


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
    public ActionResult<List<string>> GetRawMaterial(int materialNumber, string vendor)
    {
        ProductLot product = RawMaterialServices.NewRun(materialNumber, vendor);

        if (product == null)
            return NotFound();
        return product.RawMaterial;
    }
    [HttpGet("(DrumId)")]
    public ActionResult<RawMaterialDTO> GetRawMaterialInfo(string drumId) => ProductServices.GetRawMaterial(drumId);

}