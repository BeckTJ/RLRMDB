using System;
using System.Collections.Generic;

namespace RavenDAL.Models
{
    public partial class SampleSubmit
    {
        public SampleSubmit()
        {
            Productions = new HashSet<Production>();
            RawMaterials = new HashSet<RawMaterial>();
            VendorBatches = new HashSet<VendorBatch>();
        }

        public string SampleSubmitNumber { get; set; } = null!;
        public long? InspectionLotNumber { get; set; }
        public DateTime? SampleDate { get; set; }
        public bool? Rejected { get; set; }
        public DateTime? ReviewDate { get; set; }
        public DateTime? ExperiationDate { get; set; }
        public string? EmployeeId { get; set; }

        public virtual Employee? Employee { get; set; }
        public virtual ICollection<Production> Productions { get; set; }
        public virtual ICollection<RawMaterial> RawMaterials { get; set; }
        public virtual ICollection<VendorBatch> VendorBatches { get; set; }
    }
}
