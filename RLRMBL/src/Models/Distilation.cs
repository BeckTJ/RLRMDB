using System;
using System.Collections.Generic;

namespace RLRMBL.Models
{
    public partial class Distilation
    {
        public int ProductId { get; set; }
        public string? ProductLotNumber { get; set; }
        public string? DrumLotNumber { get; set; }
        public int? RawMaterialLoaded { get; set; }
        public int? Prefraction { get; set; }
        public int? Heels { get; set; }
        public bool? HeelsPumped { get; set; }
        public int? RunNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual RawMaterial? DrumLotNumberNavigation { get; set; }
        public virtual Production? ProductLotNumberNavigation { get; set; }
    }
}
