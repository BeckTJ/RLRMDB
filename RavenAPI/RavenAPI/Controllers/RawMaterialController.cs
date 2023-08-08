using Microsoft.AspNetCore.Mvc;
using RavenDAL.DTO;
using RavenBAL.Interface;
using RavenBAL.Repository;
using RavenBAL.src;

namespace RavenAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class RawMaterialController : ControllerBase
{
    private readonly IRawMaterial<RawMaterialData> _rawMaterial;
    private readonly IVendorLot<VendorLot> _vendorLot;

    public RawMaterialController(IRawMaterial<RawMaterialData> rawMaterial, IVendorLot<VendorLot> vendorLot)
    {
        _rawMaterial = rawMaterial;
        _vendorLot = vendorLot;
    }

    [HttpGet]
    public ActionResult<List<VendorLot>> GetAll(int materialNumber) => _vendorLot.GetAll(materialNumber).ToList();

    [HttpGet("(ParentMaterialNumber)")]
    public ActionResult<List<RawMaterialData>> GetAllRawMaterial(int parentMaterialNumber) 
    {
        var rawMaterial = _rawMaterial.GetAllRawMaterial(parentMaterialNumber).ToList();

        if (rawMaterial == null)
            return NotFound();
        return rawMaterial;
    }

    //[HttpGet("(RawMaterialByMaterialNumber)")]
    //public ActionResult<List<RawMaterialDTO>> Get(int materialNumber)
    //{
    //    var rawMaterial = RawMaterialServices.Get(materialNumber);
    //
    //    if (rawMaterial == null)
    //        return NotFound();
    //    return rawMaterial;
    //}
    //
    //[HttpGet("(RawMaterial)")]
    //public ActionResult<object> GetRawMaterial(int materialNumber, string vendor)
    //{
    //    var rawMaterial = RawMaterialServices.RawMaterialSelection(materialNumber, vendor);
    //
    //    if (rawMaterial == null)
    //        return NotFound();
    //    return rawMaterial;
    //}
    //
    //[HttpGet("(VendorBatchNumberForRawMaterialDrumId)")]
    //public ActionResult<RawMaterialDTO> GetRawMaterialDrumId(string drumId) => RawMaterialServices.GetRawMaterial(drumId);

}