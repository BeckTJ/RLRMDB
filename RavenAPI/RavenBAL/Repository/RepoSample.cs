
using RavenBAL.Interface;
using RavenBAL.src;
using RavenDAL.DTO;
using RavenDAL.Interface;
using RavenDAL.Models;

namespace RavenBAL.Repository
{
    public class RepoSample : ISample<Sample>
    {
        private readonly ISampleSubmit<SampleDTO> _sample;
        public RepoSample(ISampleSubmit<SampleDTO> sample) 
        {
            _sample = sample;
        }
        public Sample GetSample(string sampleId) 
        {
            var sample = _sample.GetBySampleNumber(sampleId);
            return new Sample
            {
                SampleId = sample.SampleSubmitNumber,
                SampleDate = (DateTime)sample.SampleDate,
                Approved = (bool)sample.Approved,
                Rejected = (bool)sample.Rejected,
                ReviewDate = (DateTime)sample.ReviewDate,
                ExperationDate = (DateTime)sample.ExperationDate,
            };
        }
        public bool SampleApproved(string sampleId)
        {
            return GetSample(sampleId).Approved;
        }
    }
}