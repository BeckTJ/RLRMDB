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
    public class RawMaterialDrum
    {
        public int MaterialNumber { get; set; }
        public string ProductId { get; set; }
        public string VendorLotNumber { get; set; }
        public string ContainerNumber { get; set; }
        public int Weight { get; set; }
        public string SampleId { get; set; }
        public int BatchNumber { get; set; }
    }
}
