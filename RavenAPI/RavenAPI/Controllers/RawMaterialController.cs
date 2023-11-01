using AutoMapper;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using RavenDAL.DTO;
using RavenDAL.Models;

namespace RavenAPI.Controllers;

[Route("RavenAPI/RawMaterial")]
[ApiController]

public class RawMaterialController : ControllerBase
{
    private ILoggerManager _log;
    private IRepoWrapper _repo;
    private IMapper _mapper;

    public RawMaterialController(ILoggerManager log, IRepoWrapper repo, IMapper mapper)
    {
        _log = log;
        _repo = repo;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAllRawMaterial()
    {
        try
        {
            var material = _repo.Vendor.GetAllVendors();
            _log.LogInfo($"Return all material from database.");

            var rawMaterial = _mapper.Map<IEnumerable<VendorLotDTO>>(material);
            return Ok(rawMaterial);
        }
        catch (Exception ex)
        {
            _log.LogError($"Something went wrong inside GetAllRawMaterial action: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }
    [HttpGet("{materialNumber}", Name = "RawMaterialByMaterialNumber")]
    public IActionResult GetRawMaterials(int materialNumber)
    {
        try
        {
            var vendor = _repo.Vendor.GetVendorLotsWithRawMaterials(materialNumber);

            if (vendor == null)
            {
                _log.LogError($"Vendor with material number: {materialNumber}, hasn't been found in the db");
                return NotFound();
            }
            else
            {
                _log.LogInfo($"Returned vendor with raw material for id: {materialNumber}");

                var rawMaterial = _mapper.Map<List<VendorLotDTO>>(vendor);
                return Ok(rawMaterial);
            }
        }
        catch (Exception ex)
        {
            _log.LogError($"Something went wrong inside GetVendorWithRawMaterial action: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }
    [HttpPost]
    public IActionResult CreateRawMaterial([FromBody]CreateRawMaterialDTO material)
    { //Need to build ProductId before I can create raw material
        try
        {
            if(material is null)
            {
                _log.LogError("Raw Material from client is null");
                return BadRequest("Raw Material is Null");
            }
            if (!ModelState.IsValid)
            {
                _log.LogError("Invalid Raw Material Object");
                return BadRequest("Invalid object");
            }
            var rawMaterial = _mapper.Map<RawMaterial>(material);

            _repo.Save();

            var createdRawMaterial = _mapper.Map<RawMaterial>(rawMaterial);
            return CreatedAtRoute("RawMaterialByMaterialNumber", new { id = createdRawMaterial.ProductId }, createdRawMaterial);
        }
        catch (Exception ex)
        {
            _log.LogError($"Something went wrong inside CreateRawMaterial action:{ex.Message}");
            return StatusCode(500, $"Internal sever error\t{ex.Message}");
        }
    }
}