using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public record CreateVendorLotDTO
    {
        public int MaterialNumber { get; set; } // DB alt key
        public string? VendorLotNumber { get; set; }
        public int Quantity { get; set; }
        public string? SampleId { get; set; }
    }
}
