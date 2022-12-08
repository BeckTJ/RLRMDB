using System;
using System.Collections.Generic;

namespace RavenAPI.Models
{
    public partial class Material
    {
        public Material()
        {
            IndicatorSetPoints = new HashSet<IndicatorSetPoint>();
            MaterialNumbers = new HashSet<MaterialNumber>();
        }

        public int MaterialNumber { get; set; }
        public string MaterialName { get; set; } = null!;
        public string? MaterialNameAbreviation { get; set; }
        public string? PermitNumber { get; set; }
        public string? RawMaterialCode { get; set; }
        public string? ProductCode { get; set; }
        public bool CarbonDrumRequired { get; set; }
        public int? CarbonDrumDaysAllowed { get; set; }
        public int? CarbonDrumWeightAllowed { get; set; }
        public DateTime? CarbonDrumInstallDate { get; set; }
        public decimal? SpecificGravity { get; set; }
        public string? PrefractionRefluxRatio { get; set; }
        public string? CollectRefluxRatio { get; set; }
        public int? NumberOfRuns { get; set; }

        public virtual ICollection<IndicatorSetPoint> IndicatorSetPoints { get; set; }
        public virtual ICollection<MaterialNumber> MaterialNumbers { get; set; }
    }
}
