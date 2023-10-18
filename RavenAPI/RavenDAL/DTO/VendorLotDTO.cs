using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RavenDAL.DTO
{
    public class VendorLotDTO
    {
        public int MaterialNumber { get; set; }
        public string Name { get; set; }
        public string VendorLotNumber { get; set; }
        public int Quantity { get; set; }
        public string SampleSubmitNumber { get; set; }
        public IEnumerable<RawMaterialDTO> RawMaterials { get; set; }
    }
}
