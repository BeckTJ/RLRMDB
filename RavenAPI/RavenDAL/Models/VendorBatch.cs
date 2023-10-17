using System;
using System.Collections.Generic;

namespace RavenDAL.Models
{
    public partial class VendorBatch
    {
        public VendorBatch()
        {
            RawMaterials = new HashSet<RawMaterial>();
        }

        public string VendorLotNumber { get; set; }
        public string? VendorName { get; set; }
        public string? SampleId { get; set; }
        public int? Quantity { get; set; }
        public int? MaterialNumber { get; set; }

        public virtual MaterialNumber? MaterialNumberNavigation { get; set; }
        public virtual SampleSubmit? Sample { get; set; }
        public virtual Vendor? VendorNameNavigation { get; set; }
        public virtual ICollection<RawMaterial>? RawMaterials { get; set; }
    }
}
