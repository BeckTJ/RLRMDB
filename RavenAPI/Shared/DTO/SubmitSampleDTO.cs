using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public record SubmitSampleDTO
    {
        public int SampleId { get; set; }
        public string SampleType { get; set; }
        public DateTime SampleDate { get; set; }
    }
}
