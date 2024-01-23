using RavenDB.Models;

namespace Contracts
{
    public interface ISampleRepo : IRepoBase<SampleSubmit>
    {
        void SubmitSample(SampleSubmit sample);
        Task<SampleSubmit> VerifySample(string sampleType, int sampleId);

    }
}
