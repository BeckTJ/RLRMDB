using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public record VendorLotWithSampleDTO
    {
        public int MaterialNumber { get; set; }
        public string? Name { get; set; }
        public string? VendorLotNumber { get; set; }
        public int Quantity { get; set; }
        public SampleDTO? SampleSubmitNumber { get; set; }
        public IEnumerable<RawMaterialDrumDTO>? RawMaterialDrums { get; set; }

    }
}
