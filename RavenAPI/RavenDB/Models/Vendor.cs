using System;
using System.Collections.Generic;

namespace RavenDB.Models;

public partial class Vendor
{
    public string VendorName { get; set; } = null!;

    public virtual ICollection<MaterialVendor> MaterialVendors { get; set; } = new List<MaterialVendor>();
}
