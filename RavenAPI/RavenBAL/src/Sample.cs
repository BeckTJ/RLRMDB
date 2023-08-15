using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenBAL.src
{
    public class Sample
    {
        public string SampleId { get; set; }
        public DateTime SampleDate { get; set; }
        public bool Approved { get; set; }
        public bool Rejected { get; set; }
        public DateTime ReviewDate { get; set; }
        public DateTime ExperationDate { get; set; }
    }
}
