using System;
using System.Collections.Generic;

namespace RLRMBL
{
    public partial class QualityControl
    {
        public QualityControl()
        {
            Products = new HashSet<Product>();
            RawMaterials = new HashSet<RawMaterial>();
        }

        public string SampleSubmitNumber { get; set; } = null!;
        public decimal? InspectionLotNumber { get; set; }
        public int? VendorBatchId { get; set; }
        public bool? Rejected { get; set; }
        public DateTime? RejectedDate { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public DateTime? ExperiationDate { get; set; }

        public virtual VendorBatchInformation? VendorBatch { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<RawMaterial> RawMaterials { get; set; }
    }
}
