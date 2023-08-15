using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenBAL.src
{
    public class HighPurityMaterial : MaterialInfo
    {
        public string MaterialName { get; set; }
        public string MaterialNameAbrev { get; set; }
        public string PermitNumber { get; set; }
        public bool BatchManaged { get; set; }
        public decimal SpecificGravity { get; set; } 
        public List<MaterialVendor> RawMaterial { get; set; }
    }
}
