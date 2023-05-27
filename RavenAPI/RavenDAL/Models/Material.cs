using System;
using System.Collections.Generic;

namespace RavenDAL.Models
{
    public partial class Material
    {
        public Material()
        {
            IndicatorSetPoints = new HashSet<IndicatorSetPoint>();
            MaterialNumbers = new HashSet<MaterialNumber>();
            PreStartChecks = new HashSet<PreStartCheck>();
            SystemReceivers = new HashSet<SystemReceiver>();
        }

        public int MaterialNumber { get; set; }
        public string MaterialName { get; set; } = null!;
        public string MaterialNameAbreviation { get; set; } = null!;
        public string? PermitNumber { get; set; }
        public bool CarbonDrumRequired { get; set; }
        public int? CarbonDrumDaysAllowed { get; set; }
        public int? CarbonDrumWeightAllowed { get; set; }
        public DateTime? CarbonDrumInstallDate { get; set; }
        public bool VacuumTrapRequired { get; set; }
        public DateTime? VacuumTrapInstallDate { get; set; }
        public int? VacuumTrapDaysAllowed { get; set; }
        public decimal? SpecificGravity { get; set; }
        public string? PrefractionRefluxRatio { get; set; }
        public string? CollectRefluxRatio { get; set; }
        public int? NumberOfRuns { get; set; }
        public int? HeelPumpFrequency { get; set; }

        public virtual ICollection<IndicatorSetPoint> IndicatorSetPoints { get; set; }
        public virtual ICollection<MaterialNumber> MaterialNumbers { get; set; }
        public virtual ICollection<PreStartCheck> PreStartChecks { get; set; }
        public virtual ICollection<SystemReceiver> SystemReceivers { get; set; }
    }
}
