using System;
using System.Collections.Generic;

namespace RavenAPI.Models
{
    public partial class ProductNumberSequence
    {
        public int SequenceId { get; set; }
        public int SequenceIdStart { get; set; }
        public int SequenceIdEnd { get; set; }
    }
}
