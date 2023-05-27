using RavenDAL.Models;
using RavenDAL.Data;
using RavenBAL.DTO;

namespace RavenBAL.Services;

public class DistillationServices
{
    static readonly RavenDBContext ctx = new();

    static List<DistillationSystemDTO> DistillationSystem { get; } = ctx.Materials
        .Select(m => new DistillationSystemDTO
        {
            SystemName = m.MaterialNameAbreviation,
            MaterialNumber = m.MaterialNumber,
            PrefractionRefluxRatio = m.PrefractionRefluxRatio,
            CollectRefluxRatio = m.CollectRefluxRatio,
            NumberOfRuns = m.NumberOfRuns,
            HeelPumpFrequency = m.HeelPumpFrequency,
            CarbonDrum = SetTrap(m.CarbonDrumRequired, m.CarbonDrumInstallDate, m.CarbonDrumDaysAllowed, m.CarbonDrumWeightAllowed),
            VacuumTrap = SetTrap(m.VacuumTrapRequired, m.VacuumTrapInstallDate, m.VacuumTrapDaysAllowed),
            SystemSetPoints = SystemSetPointDTO.SetSystemSetPointS(m.MaterialNumber),
            ReceiverNames = ReceiverDTO.SetReceivers(m.MaterialNumber),
        }).ToList();
    public static List<DistillationSystemDTO> GetAll() => DistillationSystem;
    public static DistillationSystemDTO StartDistillation(int materialNumber) => DistillationSystem.FirstOrDefault(ds => ds.MaterialNumber == materialNumber);

    static ContainmentVesselDTO SetTrap(bool isRequired, DateTime? installDate, int? daysAllowed, int? weightAllowed) => new(isRequired, installDate, daysAllowed, weightAllowed);
    static ContainmentVesselDTO SetTrap(bool isRequired, DateTime? installDate, int? daysAllowed) => new(isRequired, installDate, daysAllowed);

}