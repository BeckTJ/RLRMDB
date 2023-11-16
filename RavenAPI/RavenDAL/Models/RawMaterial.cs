using System;
using System.Collections.Generic;

namespace RavenDAL.Models;

public partial class RawMaterial
{
    public string ProductId { get; set; } = null!;

    public int? MaterialNumber { get; set; }

    public int? DrumWeight { get; set; }

    public int? SapBatchNumber { get; set; }

    public string? ContainerNumber { get; set; }

    public decimal? InspectionLotNumber { get; set; }

    public string? SampleSubmitNumber { get; set; }

    public string? VendorLotNumber { get; set; }

    public string? EmployeeId { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual MaterialVendor? MaterialNumberNavigation { get; set; }

    public virtual ICollection<ProductRun> ProductRuns { get; set; } = new List<ProductRun>();

    public virtual SampleSubmit? Sample { get; set; }

    public virtual VendorLot? VendorLot { get; set; }
}
