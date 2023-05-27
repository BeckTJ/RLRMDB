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

        public string DrumLotNumber { get; set; } = null!;
        public int? MaterialNumber { get; set; }
        public int? DrumWeight { get; set; }
        public int? SapBatchNumber { get; set; }
        public string? ContainerNumber { get; set; }
        public decimal? InspectionLotNumber { get; set; }
        public string? SampleSubmitNumber { get; set; }
        public string? VendorBatchNumber { get; set; }
        public string? EmployeeId { get; set; }

        public virtual Employee? Employee { get; set; }
        public virtual MaterialNumber? MaterialNumberNavigation { get; set; }
        public virtual SampleSubmit? SampleSubmitNumberNavigation { get; set; }
        public virtual VendorBatch? VendorBatchNumberNavigation { get; set; }
        public virtual ICollection<ProductRun> ProductRuns { get; set; }
    }
}
