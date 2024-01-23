using Contracts;
using Microsoft.EntityFrameworkCore;
using RavenDB.Data;
using RavenDB.Models;

namespace Repository.Async
{
    internal sealed class SampleRequiredRepo : RepoBase<SampleRequired>, ISampleRequiredRepo
    {
        public SampleRequiredRepo(RavenContext ctx) : base(ctx) { }

        public async Task<IEnumerable<SampleRequired>> GetSampleRequired(int parentMaterialNumber) =>
            await FindByCondition(s => s.MaterialNumber.Equals(parentMaterialNumber)).ToListAsync();
    }
}
