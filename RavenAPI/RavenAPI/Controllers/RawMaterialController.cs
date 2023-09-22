using AutoMapper;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using RavenBAL.Interface;
using RavenBAL.src;
using RavenDAL.DTO;

namespace RavenAPI.Controllers;

[Route("RavenAPI/[rawMaterial]")]
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
}