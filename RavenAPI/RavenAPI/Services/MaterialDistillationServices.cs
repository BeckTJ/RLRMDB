using Microsoft.EntityFrameworkCore;
using RavenAPI.Models;
using RavenAPI.DTO;

namespace RavenAPI.DTO;
public class MaterialDistillationServices
{
    static RavenDBContext context = new RavenDBContext();
    static List<MaterialDistillationDTO> PreStart { get; } = context.Materials
    .Select(ps => new MaterialDistillationDTO
    {
        MaterialNumber = ps.MaterialNumber,
        CarbonDrumRequired = ps.CarbonDrumRequired,
        CarbonDrumDaysAllowed = ps.CarbonDrumDaysAllowed,
        CarbonDrumInstallDate = ps.CarbonDrumInstallDate,
        // VacuumTrapRequired = ps.VacuumTrapRequierd,
        // VacuumTrapInstallDate = (DateOnly)ps.VacuumTrapInstallDate,
        // vacuumTrapDaysAllowed = ps.VacuumTrapDaysAllowed,
    }).ToList();

    public static List<MaterialDistillationDTO> GetAll() => PreStart;


    public static MaterialDistillationDTO Get(int materialNumber) => PreStart.Where(ps => ps.MaterialNumber == materialNumber).FirstOrDefault();
}