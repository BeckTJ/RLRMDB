using System;
using System.Collections.Generic;

namespace RavenAPI.Models
{
    public partial class ProductRun
    {
        public int RunId { get; set; }
        public int? RunNumber { get; set; }
        public string? DrumLotNumber { get; set; }
        public int? RawMaterialStartWeight { get; set; }
        public int? RawMaterialEndWeight { get; set; }
        public int? TotalRawMaterialLoaded { get; set; }
        public bool? KopotDrained { get; set; }
        public TimeSpan? ReadingTime { get; set; }
        public string? SystemStatus { get; set; }
        public bool? VisualVerification { get; set; }
        public int? CollectRate { get; set; }
        public int? RecieverLevel { get; set; }
        public int? HeelsLevel { get; set; }
        public bool? HeelsPumped { get; set; }
        public int? PrefractionLevel { get; set; }
        public int? ReboilerLevel { get; set; }
        public string? EmployeeId { get; set; }

        public virtual RawMaterial? DrumLotNumberNavigation { get; set; }
        public virtual Employee? Employee { get; set; }
    }
}
