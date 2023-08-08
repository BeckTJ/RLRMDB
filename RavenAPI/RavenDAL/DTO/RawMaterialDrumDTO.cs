using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDAL.DTO
{
    public class RawMaterialDrumDTO
    {
        public int? MaterialNumber { get; set; }
        public string? DrumLotNumber { get; set; }
        public int? DrumBatchNumber { get; set; }
        public string? ContainerNumber { get; set; }
        public string? VendorLotNumber { get; set; }
        public int? DrumWeight { get; set; }
    }
}
