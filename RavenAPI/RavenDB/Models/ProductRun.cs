using System;
using System.Collections.Generic;

namespace RavenDB.Models;

public partial class ProductRun
{
    public int RunId { get; set; }

    public int? RunNumber { get; set; }

    public string? ProductId { get; set; }

    public int? RawMaterialUsed { get; set; }

    public DateTime? RunStartDate { get; set; }

    public string? ProductLotNumber { get; set; }

    public string? EmployeeId { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<PreStartCheck> PreStartChecks { get; set; } = new List<PreStartCheck>();

    public virtual RawMaterial? Product { get; set; }

    public virtual ICollection<ProductLevel> ProductLevels { get; set; } = new List<ProductLevel>();

    public virtual Production? ProductLotNumberNavigation { get; set; }
}
