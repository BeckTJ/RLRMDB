using Microsoft.EntityFrameworkCore;
using RavenAPI.Models;
using RavenAPI.DTO;

namespace RavenAPI.DTO;
public class PreStartServices
{
    static RavenDBContext context = new RavenDBContext();
    static List<PreStartDTO> PreStart { get; } = context.Materials
    .Select(ps => new PreStartDTO
    {
        materialNumber = ps.MaterialNumber,
        carbonDrumRequired = ps.CarbonDrumRequired,
        carbonDrumDaysAllowed = ps.CarbonDrumDaysAllowed,
        carbonDrumInstallDate = ps.CarbonDrumInstallDate,
        // VacuumTrapRequired = ps.VacuumTrapRequierd,
        // VacuumTrapInstallDate = (DateOnly)ps.VacuumTrapInstallDate,
        // vacuumTrapDaysAllowed = ps.VacuumTrapDaysAllowed,
    }).ToList();

    public static List<PreStartDTO> GetAll() => PreStart;


    public static PreStartDTO Get(int materialNumber) => PreStart.Where(ps => ps.materialNumber == materialNumber).FirstOrDefault();
}