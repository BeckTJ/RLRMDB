using AutoMapper;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using RavenDAL.DTO;

namespace RavenAPI.Controllers;

[Route("RavenAPI/Material")]
[ApiController]

public class MaterialController : ControllerBase
{
    private ILoggerManager _log;
    private IRepoWrapper _repo;
    private IMapper _mapper;
    public MaterialController(ILoggerManager log, IRepoWrapper repo, IMapper mapper)
    {
        _repo = repo;
        _log = log;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAllMaterial()
    {
        try
        {
            var materials = _repo.Material.GetAllMaterial();
            _log.LogInfo($"Return all material from database.");

            var materialResult = _mapper.Map<IEnumerable<MaterialDTO>>(materials);
            return Ok(materials);

        }catch (Exception ex)
        {
            _log.LogError($"Something went wrong inside GetAllMaterial action: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }
    [HttpGet("(materialNumber)")]
    public IActionResult GetMaterialByMaterialNumber(int materialNumber) 
    {
        try
        {
            var material = _repo.Material.GetMaterialByMaterialNumber(materialNumber);

            if(material is null)
            {
                _log.LogError($"Material with id: {materialNumber}, hasn't been found in db.");
                return NotFound();
            }
            else
            {
                _log.LogInfo($"Returned Material with id: {materialNumber}");
                return Ok(material);
            }
        }
        catch (Exception ex)
        {
            _log.LogError($"Something went wrong inside GetMaterialByMaterialNumber action: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }
}