using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenBAL.DTO
{
    public class CheckSampleDTO
    {
        public int MaterialNumber { get; set; }
        public string? MaterialType { get; set; }
        public IEnumerable<string>? VLN { get; set; }

    }
}
