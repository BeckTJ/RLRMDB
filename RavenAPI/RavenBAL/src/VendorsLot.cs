using RavenDAL.DTO;
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
        public VendorBatchDTO VendorBatch { get; set; }
        public IEnumerable<RawMaterialDrumDTO> RawMaterialDrum { get; set; }

    }
}
