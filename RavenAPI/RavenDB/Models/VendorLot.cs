using System;
using System.Collections.Generic;

namespace RavenDB.Models;

public partial class VendorLot
{
    public string VendorLotNumber { get; set; } = null!;

    public int? SampleId { get; set; }

    public int? Quantity { get; set; }

    public int? MaterialNumber { get; set; }

    public virtual MaterialVendor? MaterialNumberNavigation { get; set; }

    public virtual ICollection<RawMaterial> RawMaterials { get; set; } = new List<RawMaterial>();

    public virtual SampleSubmit? Sample { get; set; }
}
