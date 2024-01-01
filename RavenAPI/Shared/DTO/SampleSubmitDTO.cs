using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class SampleSubmitDTO
    {
        public int SampleId { get; set; }
        public string? sampleType { get; set; }
        public DateTime? SampleDate { get; set; }
    }
}
