using System;
using System.Collections.Generic;

namespace RavenDB.Models;

public partial class SampleSubmit
{
    public string SampleSubmitNumber { get; set; } = null!;

    public long? InspectionLotNumber { get; set; }

    public DateTime SampleDate { get; set; }

    public bool Rejected { get; set; }

    public bool Approved { get; set; }

    public DateTime? ReviewDate { get; set; }

    public DateTime? ExperiationDate { get; set; }

    public string? EmployeeId { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<Production> Productions { get; set; } = new List<Production>();

    public virtual ICollection<RawMaterial> RawMaterials { get; set; } = new List<RawMaterial>();

    public virtual ICollection<VendorLot> VendorLots { get; set; } = new List<VendorLot>();
}
