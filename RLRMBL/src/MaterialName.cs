using System;
using System.Collections.Generic;

namespace RLRMBL
{
    public partial class MaterialName
    {
        public MaterialName()
        {
            MaterialNumbers = new HashSet<MaterialNumber>();
        }

        public int MaterialNameId { get; set; }
        public string? MaterialName1 { get; set; }
        public string? MaterialNameAbreviation { get; set; }
        public string? PermitNumber { get; set; }
        public string? RawMaterialCode { get; set; }
        public string? ProductCode { get; set; }
        public bool? CarbonDrumRequired { get; set; }
        public int? CarbonDrumDaysAllowed { get; set; }
        public int? CarbonDrumWeightAllowed { get; set; }

        public virtual ICollection<MaterialNumber> MaterialNumbers { get; set; }
    }
}
