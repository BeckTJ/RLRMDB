using Microsoft.AspNetCore.Mvc;
using RavenDAL.DTO;
using RavenBAL.Interface;
using RavenBAL.Repository;
using RavenBAL.src;
using RavenDAL.Models;

namespace RavenAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class RawMaterialController : ControllerBase
{
    private readonly IRawMaterialDrum<RawMaterialDrum> _rawMaterial;
    private readonly IVendorLot<VendorLot> _vendorLot;

    public RawMaterialController(IRawMaterialDrum<RawMaterialDrum> rawMaterial, IVendorLot<VendorLot> vendorLot)
    {
        _rawMaterial = rawMaterial;
        _vendorLot = vendorLot;
    }

    [HttpGet]
    public ActionResult<List<VendorLot>> GetAll(int materialNumber) => _vendorLot.GetAll(materialNumber).ToList();

    [HttpGet("(ParentMaterialNumber)")]
    public ActionResult<List<RawMaterialDrum>> GetAllRawMaterial(int parentMaterialNumber) 
    {
        var rawMaterial = _rawMaterial.GetAllRawMaterialDrum(parentMaterialNumber).ToList();

        if (rawMaterial == null)
            return NotFound();
        return rawMaterial;
    }
    [HttpGet("(Vendor)")]

    public ActionResult<List<VendorLot>> GetVendorLot(int materialNumber)
    {
        var rawMaterial = _vendorLot.GetAll(materialNumber).ToList();

        if (rawMaterial == null)
            return NotFound();
        return rawMaterial;
    }
    public ActionResult<string> GetProductId(int materialNumber, string vendorLot, string containerNumber, int weight, int batchNumber, long inspectionLotNumber)
    {
        var productId = _rawMaterial.CreateRawMaterialDrum(materialNumber, vendorLot, containerNumber, weight, batchNumber, inspectionLotNumber);

        if (productId == null)
            return NotFound();
        return productId;

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