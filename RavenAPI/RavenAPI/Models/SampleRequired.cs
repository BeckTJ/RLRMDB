using System;
using System.Collections.Generic;

namespace RavenAPI.Models
{
    public partial class SampleRequired
    {
        public int RequiredId { get; set; }
        public int? MaterialNumber { get; set; }
        public bool AmpRequired { get; set; }
        public int? NumberOfAmps { get; set; }
        public bool MetalsRequired { get; set; }
        public int? NumberOfMetals { get; set; }
        public bool RetainRequired { get; set; }

        public virtual MaterialNumber? MaterialNumberNavigation { get; set; }
    }
}
