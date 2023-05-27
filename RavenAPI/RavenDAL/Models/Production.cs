using System;
using System.Collections.Generic;

namespace RavenDAL.Models
{
    public partial class Production
    {
        public Production()
        {
            ProductLevels = new HashSet<ProductLevel>();
            ProductRuns = new HashSet<ProductRun>();
        }

        public string ProductLotNumber { get; set; } = null!;
        public int? MaterialNumber { get; set; }
        public int? ProductBatchNumber { get; set; }
        public long? ProcessOrder { get; set; }
        public long? InspectionLotNumber { get; set; }
        public string? ReceiverName { get; set; }
        public string? SampleSubmitNumber { get; set; }

        public virtual MaterialNumber? MaterialNumberNavigation { get; set; }
        public virtual Receiver? ReceiverNameNavigation { get; set; }
        public virtual SampleSubmit? SampleSubmitNumberNavigation { get; set; }
        public virtual ICollection<ProductLevel> ProductLevels { get; set; }
        public virtual ICollection<ProductRun> ProductRuns { get; set; }
    }
}
