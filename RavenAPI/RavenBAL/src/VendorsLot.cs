using RavenBAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenBAL.src
{
    public class VendorLot
    {
        public int MaterialNumber { get; set; }
        public string LotNumber { get; set; }
        public int Quantity { get; set; }
        public string SampleId { get; set; }

        public IEnumerable<RawMaterialDrum> RawMaterialDrums { get; set; }

    }
}
