using System;
using System.Collections.Generic;

namespace RavenDB.Models;

public partial class Material
{
    public int MaterialNumber { get; set; }

    public string MaterialName { get; set; } = null!;

    public string MaterialAbrev { get; set; } = null!;

    public string? PermitNumber { get; set; }

    public string UnitOfIssue { get; set; } = null!;

    public bool BatchManaged { get; set; }

    public string MaterialCode { get; set; } = null!;

    public int SequenceId { get; set; }

    public int TotalRecords { get; set; }

    public virtual ICollection<MaterialVendor> MaterialVendors { get; set; } = new List<MaterialVendor>();

    public virtual ICollection<PreStartCheck> PreStartChecks { get; set; } = new List<PreStartCheck>();

    public virtual ICollection<Production> Productions { get; set; } = new List<Production>();
}
