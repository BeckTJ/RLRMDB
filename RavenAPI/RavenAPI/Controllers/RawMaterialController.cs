using AutoMapper;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using RavenBAL.Interface;
using RavenBAL.src;
using RavenDAL.DTO;

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
            var material = _repo.RawMaterial.GetAllRawMaterial();
            _log.LogInfo($"Return all material from database.");

            var rawMaterial = _mapper.Map<IEnumerable<RawMaterialDTO>>(material);
            return Ok(rawMaterial);
        }
        catch (Exception ex)
        {
            _log.LogError($"Something went wrong inside GetAllRawMaterial action: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }
    [HttpGet("{materialNumber}/vendor")]
    public IActionResult GetVendorWithRawMaterial(int materialNumber) 
    {
        try
        {
            var vendor = _repo.Vendor.GetVendorWithRawMaterial(materialNumber);
            if (vendor == null)
            {
                _log.LogError($"Vendor with material number: {materialNumber}, hasn't been found id db.");
                return NotFound();
            }
            else
            {
                _log.LogInfo($"Returned vendor with raw material for id: {materialNumber}");

                var rawMaterial = _mapper.Map<VendorDTO>(vendor);
                return Ok(rawMaterial);
            }    
        }
        catch(Exception ex)
        {
            _log.LogError($"Something went wrong inside GetVendorWithRawMaterial action: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }
}