using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDAL.DTO
{
    public class ProductDTO
    {
        public int? MaterialNumber { get; set; }
        public string? ProductLotNumber { get; set; }
        public long? ProcessOrder { get;set; }
        public int? BatchNumber { get; set; }
        public string? Reciever { get; set; }
        public string? InspectionLotNumber { get; set; }
        public int? RecieverLevel { get; set; }
        public string? SampleId { get; set; }
    }
}
