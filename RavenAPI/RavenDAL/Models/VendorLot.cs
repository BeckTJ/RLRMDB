using System;
using System.Collections.Generic;

namespace RavenDAL.Models;

public partial class VendorLot
{
    public string VendorLotNumber { get; set; } = null!;

    public string? SampleSubmitNumber { get; set; }

    public int? Quantity { get; set; }

    public int? MaterialNumber { get; set; }

    public virtual MaterialVendor? MaterialNumberNavigation { get; set; }

    public virtual ICollection<RawMaterial> RawMaterials { get; set; } = new List<RawMaterial>();

    public virtual SampleSubmit? SampleSubmitNumberNavigation { get; set; }
}
