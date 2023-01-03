namespace RavenAPI.DTO;

public class PreStartDTO
{
    public int materialNumber { get; set; }
    public bool carbonDrumRequired { get; set; }
    public int? carbonDrumDaysAllowed { get; set; }
    public DateTime? carbonDrumInstallDate { get; set; }
    public DateTime? VacuumTrapInstallDate { get; set; }
    public int? vacuumTrapDaysAllowed { get; set; }
    public object? VacuumTrapRequired { get; set; }
}