using System;
using System.Collections.Generic;

namespace RavenAPI.Models
{
    public partial class MaterialId
    {
        public int MaterialNumber { get; set; }
        public string VendorName { get; set; } = null!;
        public int? SequenceId { get; set; }
        public int? CurrentSequenceId { get; set; }
    }
}
