using Contracts;
using RavenDAL.Data;
using RavenDAL.Models;

namespace Repository
{
    public class SampleRepo : RepoBase<SampleSubmit>, ISampleRepo
    {
        public SampleRepo(RavenContext ctx) : base(ctx) { }
        public SampleSubmit VerifySample(string lotNumber)
        {
            throw new NotImplementedException();
        }

    }
}
