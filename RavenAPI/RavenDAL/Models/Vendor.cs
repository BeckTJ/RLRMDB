using System;
using System.Collections.Generic;

namespace RavenDAL.Models
{
    public partial class Vendor
    {
        public Vendor()
        {
            VendorBatches = new HashSet<VendorBatch>();
        }

        public string VendorName { get; set; } = null!;

        public virtual ICollection<VendorBatch> VendorBatches { get; set; }
    }
}
