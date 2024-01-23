using Contracts;
using Microsoft.EntityFrameworkCore;
using RavenDB.Data;
using RavenDB.Models;

namespace Repository.Async
{
    internal sealed class SampleRepo : RepoBase<SampleSubmit>, ISampleRepo
    {
        public SampleRepo(RavenContext ctx) : base(ctx) { }

        public void SubmitSample(SampleSubmit sampleSubmit)
        {
            Create(sampleSubmit);
        }
        public async Task<SampleSubmit> VerifySample(string sampleType, int sampleId) =>
            await FindByCondition(x => x.SampleId == sampleId && x.SampleType == sampleType).FirstOrDefaultAsync();
    }
}
