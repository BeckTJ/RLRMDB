using Contracts;
using Microsoft.AspNetCore.Mvc;
using RavenDB.Models;
using Shared.DTO;

namespace Presentation.Controllers;

[Route("RavenAPI/RawMaterial")]
[ApiController]

public class RawMaterialController : ControllerBase
{
    private readonly ILoggerManager _log;
    private readonly IRepoWrapper _repo;

    public RawMaterialController(ILoggerManager log, IRepoWrapper repo)
    {
        _log = log;
        _repo = repo;
    }

    [HttpGet]
    public IActionResult GetAllRawMaterial()
    {
        try
        {
            var material = _repo.Vendor.GetAllVendors();
            _log.LogInfo($"Return all material from database.");

            return Ok(material);
        }
        catch (Exception ex)
        {
            _log.LogError($"Something went wrong inside GetAllRawMaterial action: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }
    [HttpGet("{materialNumber}", Name = "RawMaterialByMaterialNumber")]
    public IActionResult GetRawMaterial(int materialNumber)
    {
        try
        {
            var vendor = _repo.MaterialVendor.GetMaterialVendorsWithVendorLot(materialNumber);

            if (vendor == null)
            {
                _log.LogError($"Vendor with material number: {materialNumber}, hasn't been found in the db");
                return NotFound();
            }
            else
            {
                _log.LogInfo($"Returned vendor with raw material for id: {materialNumber}");

                return Ok(vendor);
            }
        }
        catch (Exception ex)
        {
            _log.LogError($"Something went wrong inside GetVendorWithRawMaterial action: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }
    //[HttpPost]
    //public IActionResult CreateRawMaterial([FromBody]CreateRawMaterialDTO material)
    //{ //Need to build ProductId before I can create raw material
    //    try
    //    {
    //        if(material is null)
    //        {
    //            _log.LogError("Raw Material from client is null");
    //            return BadRequest("Raw Material is Null");
    //        }
    //        if (!ModelState.IsValid)
    //        {
    //            _log.LogError("Invalid Raw Material Object");
    //            return BadRequest("Invalid object");
    //        }
    //        
    //        _repo.Save();
    //
    //        return CreatedAtRoute("RawMaterialByMaterialNumber", new { id = material.ProductId }, createdRawMaterial);
    //    }
    //    catch (Exception ex)
    //    {
    //        _log.LogError($"Something went wrong inside CreateRawMaterial action:{ex.Message}");
    //        return StatusCode(500, $"Internal sever error\t{ex.Message}");
    //    }
    //}
}