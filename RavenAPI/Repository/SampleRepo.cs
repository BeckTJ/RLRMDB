using Contracts;
using RavenDB.Data;
using RavenDB.Models;

namespace Repository
{
    internal sealed class SampleRepo : RepoBase<SampleSubmit>, ISampleRepo
    {
        public SampleRepo(RavenContext ctx) : base(ctx) { }

        public void SubmitSample(SampleSubmit sampleSubmit)
        {
            Create(sampleSubmit);
        }
        public SampleSubmit VerifySample(string sampleType,int sampleId)
        {
            return FindByCondition(x => x.SampleId == sampleId && x.SampleType == sampleType).FirstOrDefault();
        }
    }
}
