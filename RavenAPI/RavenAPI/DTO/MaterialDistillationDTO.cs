using RavenAPI.Models;

namespace RavenAPI.DTO;

public class MaterialDistillationDTO : MaterialDTO
{
    public bool CarbonDrumRequired { get; set; }
    public int? CarbonDrumDaysAllowed { get; set; }
    public int? CarbonDrumWeightAllowed { get; set; }
    public DateTime? CarbonDrumInstallDate { get; set; }
    public bool VacuumTrapRequired { get; set; }
    public DateTime? VacuumTrapInstallDate { get; set; }
    public int? VacuumTrapDaysAllowed { get; set; }
    public decimal? SpecificGravity { get; set; }
    public string? PrefractionRefluxRatio { get; set; }
    public string? CollectRefluxRatio { get; set; }
    public int? NumberOfRuns { get; set; }
    public int? HeelPumpFrequency { get; set; }
}