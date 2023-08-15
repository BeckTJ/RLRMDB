using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenBAL.Interface
{
    public interface ISample<T> where T : class
    {
        public T GetSample(string sampleId);
        public bool SampleApproved(string sampleId);

    }
}
