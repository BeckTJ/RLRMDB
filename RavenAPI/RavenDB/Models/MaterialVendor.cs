using System;
using System.Collections.Generic;

namespace RavenDB.Models;

public partial class MaterialVendor
{
    public int MaterialNumber { get; set; }

    public string? VendorName { get; set; }

    public int? ParentMaterialNumber { get; set; }

    public string MaterialCode { get; set; } = null!;

    public int SequenceId { get; set; }

    public int CurrentSequenceId { get; set; }

    public int TotalRecords { get; set; }

    public string? UnitOfIssue { get; set; }
    public bool ContainerRequired { get; set; }
    public bool BatchManaged { get; set; }

    public bool ProcessOrderRequired { get; set; }

    public virtual Material? ParentMaterialNumberNavigation { get; set; }

    public virtual ICollection<RawMaterial> RawMaterials { get; set; } = new List<RawMaterial>();

    public virtual ICollection<VendorLot> VendorLots { get; set; } = new List<VendorLot>();
}
