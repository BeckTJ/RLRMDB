using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDAL.DTO
{
    public class SampleRequiredDTO 
    {
        public int MaterialNumber { get; set; }
        public string MaterialType { get; set; }
        public List<string> VLN { get; set; }
    }
}
