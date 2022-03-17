using System;
using System.Collections.Generic;

namespace RLRMBL
{
    public partial class VendorBatchInformation
    {
        public VendorBatchInformation()
        {
            QualityControls = new HashSet<QualityControl>();
            RawMaterials = new HashSet<RawMaterial>();
        }

        public int BatchId { get; set; }
        public int? VendorId { get; set; }
        public string? VendorBatchNumber { get; set; }
        public int? Quantity { get; set; }

        public virtual Vendor? Vendor { get; set; }
        public virtual ICollection<QualityControl> QualityControls { get; set; }
        public virtual ICollection<RawMaterial> RawMaterials { get; set; }
    }
}
