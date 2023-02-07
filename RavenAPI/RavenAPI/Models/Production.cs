using System;
using System.Collections.Generic;

namespace RavenAPI.Models
{
    public partial class Production
    {
        public Production()
        {
            ProductRuns = new HashSet<ProductRun>();
        }

        public string ProductLotNumber { get; set; } = null!;
        public int? MaterialNumber { get; set; }
        public int? ProductBatchNumber { get; set; }
        public long? ProcessOrder { get; set; }
        public long? InspectionLotNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public int? ReceiverId { get; set; }
        public string? SampleSubmitNumber { get; set; }

        public virtual MaterialNumber? MaterialNumberNavigation { get; set; }
        public virtual Receiver? Receiver { get; set; }
        public virtual SampleSubmit? SampleSubmitNumberNavigation { get; set; }
        public virtual ICollection<ProductRun> ProductRuns { get; set; }
    }
}
