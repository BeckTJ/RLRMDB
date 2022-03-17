using System;
using System.Collections.Generic;

namespace RLRMBL
{
    public partial class MaterialId
    {
        public int MaterialId1 { get; set; }
        public int? MaterialNumber { get; set; }
        public int? VendorId { get; set; }
        public int? SequenceId { get; set; }
        public int? CurrentSequenceId { get; set; }

        public virtual MaterialNumber? MaterialNumberNavigation { get; set; }
    }
}
