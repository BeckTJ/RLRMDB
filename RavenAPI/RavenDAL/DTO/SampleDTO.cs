using RavenDAL.Models;
using RavenDAL.Data;

namespace RavenDAL.DTO;

public class SampleDTO
{
    public string? SampleSubmitNumber { get; set; }
    public long? InspectionLotNumber { get; set; }
    public DateTime? SampleDate { get; set; }
    public bool? Approved { get; set; }
    public bool? Rejected { get; set; }
    public DateTime? ReviewDate { get; set; }
    public DateTime? ExperationDate { get; set; }
}

