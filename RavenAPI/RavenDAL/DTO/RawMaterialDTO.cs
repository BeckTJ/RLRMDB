using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDAL.DTO
{
    public class RawMaterialDTO
    {
        public string ProductId { get; set; }
        public int BatchNumber { get; set; }
        public string ContainerNumber { get; set; }
        public string SampleId { get; set; } 
        public int? DrumWeight { get; set; }
        public long InspectionLotNumber { get; set; }
    }
}