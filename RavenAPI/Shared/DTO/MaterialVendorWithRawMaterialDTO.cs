using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public record MaterialVendorWithRawMaterialDTO
    {
        public int ParentMaterialNumber { get; set; }
        public int MaterialNumber { get; set; }
        public string? VendorName { get; set; }
        public IEnumerable<RawMaterialDrumDTO>? RawMaterials { get; set; }

    }
}
