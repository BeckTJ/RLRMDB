using RavenDAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDAL.Interface
{
    public interface ISampleSubmit<T> where T : class
    {
        public Task<SampleDTO> SubmitSample(SampleDTO _object);
        public SampleDTO GetByInspectionLotNumber(long id);
        public SampleDTO GetBySampleNumber(string sampleId);

    }
}
