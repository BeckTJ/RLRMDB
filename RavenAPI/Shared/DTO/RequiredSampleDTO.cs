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
        public SampleContainer Amp { get; set; }
        public SampleContainer MetalBubbler { get; set; }
        public SampleContainer Vial { get; set; }
        public int AssayBulb { get; set; }
        public int Retain { get; set; }
    }
}
