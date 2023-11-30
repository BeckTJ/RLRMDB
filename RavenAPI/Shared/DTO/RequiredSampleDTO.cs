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
        public bool? Assay { get; set; }
        public bool? Water { get; set; }
        public bool? Metals { get; set; }
        public bool? Chloride { get; set; }
        public bool? Boron { get; set; }
        public bool? Phosphorus { get; set; }
        public int? Amps { get; set; }
        public string? AmpSampleSize { get; set; }
        public int? MetalBubbler { get; set; }
        public string? BubblerSampleSize{ get; set; }
        public int? AssayBulb { get; set; }
        public int? Vials { get; set; }
        public string? VialSampleSize { get; set; }
        public int? Retain { get; set; }
    }
}
