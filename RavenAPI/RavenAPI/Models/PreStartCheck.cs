using System;
using System.Collections.Generic;

namespace RavenAPI.Models
{
    public partial class PreStartCheck
    {
        public int CheckId { get; set; }
        public bool? ReboilerSkinTempBelowValue { get; set; }
        public bool? KnockOutPotDrained { get; set; }
        public bool? HeelsPumped { get; set; }
        public int? HeliumCylinderPsi { get; set; }
        public int? HeliumFlowPsi { get; set; }
        public bool? CoolantLevel { get; set; }
        public bool? CoolantPurgeSet { get; set; }
        public int? NitrogenFlowRate { get; set; }
        public int? NitrogenPurge { get; set; }
        public int? HeatingMantlePurgeSet { get; set; }
        public int? NitrogenFlow { get; set; }
        public int? AftercoolerPressure { get; set; }
        public int? ChillerSetting { get; set; }
        public int? NitrogenToCondenserPurge { get; set; }
        public bool? SecondaryPurgeSet { get; set; }
        public bool? InspectLines { get; set; }
        public bool? ControllerInitialSetBelowValue { get; set; }
        public int? RunId { get; set; }
        public int? MaterialNumber { get; set; }

        public virtual Material? MaterialNumberNavigation { get; set; }
        public virtual ProductRun? Run { get; set; }
    }
}
