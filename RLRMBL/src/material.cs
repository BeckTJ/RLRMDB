using System;
using System.Collections.Generic;

namespace RLRMBL
{
    public partial class Material
    {
        public int MaterialNumber { get; set; }
        public string? Material1 { get; set; }
        public string? Grade { get; set; }
        public string? Description { get; set; }
        public string? PermitNumber { get; set; }
        public string? RawMaterialCode { get; set; }
        public string? ProductCode { get; set; }
        public bool? CarbonDrum { get; set; }
        public int? DaysAllowed { get; set; }
        public int? WeightAllowed { get; set; }
        public bool? BatchManaged { get; set; }
        public bool? PoRequired { get; set; }
        public string? Ui { get; set; }
        public string VendorName { get; set; } = null!;
        public bool? RawMaterial { get; set; }
    }
}
