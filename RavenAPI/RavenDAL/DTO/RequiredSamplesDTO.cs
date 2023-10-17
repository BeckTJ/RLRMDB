using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDAL.DTO
{
    public class RequiredSamplesDTO 
    {
        public bool Assay { get; set; }
        public bool Water { get; set; }
        public bool Metals { get; set; }
        public bool Chloride { get; set; }
        public bool Boron { get; set; }
        public bool Phosphorus { get; set; }
        public int Amps { get; set; }
        public int AmpVolume { get; set; }
        public string AmpUnitOfIssue { get; set; }
        public int MetalBubbler { get; set; }
        public int BubblerVolume { get; set; }
        public string BubblerUnitOfIssue { get; set; }
        public int AssayBulb { get; set; }
        public int Vials { get; set; }
        public int VialVolume { get; set; }
        public string VialUnitOfIssue { get; set; }
        public int Retain { get; set; }
    }
}
