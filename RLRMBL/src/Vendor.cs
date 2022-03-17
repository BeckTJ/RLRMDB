using System;
using System.Collections.Generic;

namespace RLRMBL
{
    public partial class Vendor
    {
        public Vendor()
        {
            RawMaterials = new HashSet<RawMaterial>();
            VendorBatchInformations = new HashSet<VendorBatchInformation>();
        }

        public int VendorId { get; set; }
        public string VendorName { get; set; } = null!;

        public virtual ICollection<RawMaterial> RawMaterials { get; set; }
        public virtual ICollection<VendorBatchInformation> VendorBatchInformations { get; set; }
    }
}
