namespace RavenBAL.DTO;

public class ContainmentVesselDTO
{
    public bool IsRequired { get; set; }
    public int? DaysAllowed { get; set; }
    public int? WeightAllowed { get; set; }
    public DateTime? InstallDate { get; set; }

    public ContainmentVesselDTO(bool isRequired, DateTime? installDate, int? daysAllowed, int? weightAllowed)
    {
        IsRequired = isRequired;
        InstallDate = installDate;
        DaysAllowed = daysAllowed;
        WeightAllowed = weightAllowed;
    }
    public ContainmentVesselDTO(bool isRequired, DateTime? installDate, int? daysAllowed)
    {
        IsRequired = isRequired;
        InstallDate = installDate;
        DaysAllowed = daysAllowed;
    }
}