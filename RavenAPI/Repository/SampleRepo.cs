using Contracts;
using RavenDB.Data;
using RavenDB.Models;

namespace Repository
{
    public class SampleRepo : RepoBase<SampleSubmit>, ISampleRepo
    {
        public SampleRepo(RavenContext ctx) : base(ctx) { }
        public SampleSubmit VerifySample(string sampleId)
        {
            return FindByCondition(x => x.SampleSubmitNumber == sampleId).FirstOrDefault();
        }

    }
}
