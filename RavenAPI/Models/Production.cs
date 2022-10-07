using System;
using System.Collections.Generic;

namespace RavenAPI.Models
{
    public partial class Production
    {
        public int ProductId { get; set; }
        public string? ProductLotNumber { get; set; }
        public int? MaterialNumber { get; set; }
        public int? ProductBatchNumber { get; set; }
        public decimal? ProcessOrder { get; set; }
        public int? ReceiverId { get; set; }
        public string? SampleSubmitNumber { get; set; }
        public DateTime? StartDate { get; set; }

        public virtual MaterialNumber? MaterialNumberNavigation { get; set; }
        public virtual Receiver? Receiver { get; set; }
        public virtual SampleSubmit? SampleSubmitNumberNavigation { get; set; }
    }
}
