using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDAL.DTO
{
    public class VendorBatchDTO : VendorDTO
    {
        public string? VendorLotNumber { get; set; }
        public int? Quantity { get; set; }
        public string? SampleId { get; set; }
    }
}
