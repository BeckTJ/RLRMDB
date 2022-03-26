using System;
using System.Collections.Generic;

namespace RLRMBL.Models
{
    public partial class RawMaterial
    {
        public RawMaterial()
        {
            Distilations = new HashSet<Distilation>();
        }

        public string DrumLotNumber { get; set; } = null!;
        public int? MaterialNumber { get; set; }
        public int? DrumWeight { get; set; }
        public int? SapBatchNumber { get; set; }
        public string? ContainerNumber { get; set; }
        public string? SampleSubmitNumber { get; set; }
        public decimal? ProcessOrder { get; set; }
        public int? VendorId { get; set; }
        public int? VendorBatchId { get; set; }
        public DateTime DateUsed { get; set; }

        public virtual MaterialNumber? MaterialNumberNavigation { get; set; }
        public virtual QualityControl? SampleSubmitNumberNavigation { get; set; }
        public virtual Vendor? Vendor { get; set; }
        public virtual VendorBatchInformation? VendorBatch { get; set; }
        public virtual ICollection<Distilation> Distilations { get; set; }
    }
}
