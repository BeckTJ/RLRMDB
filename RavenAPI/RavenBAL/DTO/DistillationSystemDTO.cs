using RavenDAL.Models;

namespace RavenBAL.DTO;

public class DistillationSystemDTO
{
    public string? SystemName { get; set; }
    public int? MaterialNumber { get; set; }
    public string? PrefractionRefluxRatio { get; set; }
    public string? CollectRefluxRatio { get; set; }
    public int? NumberOfRuns { get; set; }
    public int? HeelPumpFrequency { get; set; }

    public ContainmentVesselDTO? CarbonDrum { get; set; }
    public ContainmentVesselDTO? VacuumTrap { get; set; }

    public List<ReceiverDTO>? ReceiverNames { get; set; }
    public List<SystemSetPointDTO>? SystemSetPoints { get; set; }


}