using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenBAL.src
{
    public class MaterialVendor
    {
        public int MaterialNumber { get; set; }
        public string Name { get; set; }
        public List<VendorLot> Lots { get; set; }
    }
}
