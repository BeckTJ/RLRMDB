using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public record RequiredSampleDTO
    {
        public int MaterialNumber { get; set; }
        public string? ProductLotNumber { get; set; }
        public string? VendorLotNumber { get; set; }
        public string? SampleId { get; set; }
        public long InspectionLotNumber { get; set; }
        public int NumberOfAmps { get; set; }
        public string? AmpSampleSize { get; set; }
        public int NumberOfMetalBubbler { get; set; }
        public string? BubblerSampleSize{ get; set; }
        public int AssayBulb { get; set; }
        public int NumberOfVials { get; set; }
        public string? VialSampleSize { get; set; }
        public int Retain { get; set; }
    }
}
