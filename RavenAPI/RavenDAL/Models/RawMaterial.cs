using System;
using System.Collections.Generic;

namespace RavenDAL.Models
{
    public partial class RawMaterial
    {
        public RawMaterial()
        {
            ProductRuns = new HashSet<ProductRun>();
        }

        public string ProductId { get; set; }
        public int MaterialNumber { get; set; }
        public int? DrumWeight { get; set; }
        public int? SapBatchNumber { get; set; }
        public string? ContainerNumber { get; set; }
        public long? InspectionLotNumber { get; set; }
        public string? SampleId { get; set; }
        public string VendorLotNumber { get; set; }
        public string? EmployeeId { get; set; }

        public virtual Employee? Employee { get; set; }
        public virtual MaterialNumber? MaterialNumberNavigation { get; set; }
        public virtual SampleSubmit? SampleSubmitNumberNavigation { get; set; }
        public virtual VendorBatch? VendorLotNumberNavigation { get; set; }
        public virtual ICollection<ProductRun> ProductRuns { get; set; }
    }
}
