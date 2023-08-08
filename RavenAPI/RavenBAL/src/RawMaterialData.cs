using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using RavenBAL.Interface;
using RavenDAL.DTO;
using RavenDAL.Interface;

namespace RavenBAL.src
{
    public class RawMaterialData
    {
        public MaterialDTO MaterialData { get; set; }
        public List<VendorLot> VendorLot { get; set; }
    }
}
