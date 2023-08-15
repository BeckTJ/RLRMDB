using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDAL.Interface
{
    public interface IRequiredSample<T>
    {
        public T GetSampleType();
    }
}
