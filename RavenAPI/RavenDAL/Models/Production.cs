using System;
using System.Collections.Generic;

namespace RavenDB.Models;

public partial class Production
{
    public string ProductLotNumber { get; set; } = null!;

    public int? MaterialNumber { get; set; }

    public int? ProductBatchNumber { get; set; }

    public decimal? ProcessOrder { get; set; }

    public decimal? InspectionLotNumber { get; set; }

    public string? ReceiverName { get; set; }

    public int? SampleId { get; set; }

    public virtual Material? MaterialNumberNavigation { get; set; }

    public virtual ICollection<ProductLevel> ProductLevels { get; set; } = new List<ProductLevel>();

    public virtual ICollection<ProductRun> ProductRuns { get; set; } = new List<ProductRun>();

    public virtual SampleSubmit? Sample { get; set; }
}
