using System;
using System.Collections.Generic;

namespace RLRMBL.Models
{
    public partial class Production
    {
        public Production()
        {
            Distilations = new HashSet<Distilation>();
        }

        public string ProductLotNumber { get; set; } = null!;
        public int? MaterialNumber { get; set; }
        public int? ProductionBatchNumber { get; set; }
        public decimal? ProcessOrder { get; set; }
        public int? ReceiverId { get; set; }
        public string? SampleSubmitNumber { get; set; }
        public int? Quantity { get; set; }

        public virtual MaterialNumber? MaterialNumberNavigation { get; set; }
        public virtual QualityControl? SampleSubmitNumberNavigation { get; set; }
        public virtual ICollection<Distilation> Distilations { get; set; }
    }
}
