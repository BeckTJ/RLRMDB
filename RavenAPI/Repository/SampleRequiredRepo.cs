using Contracts;
using RavenDAL.Data;
using RavenDAL.Models;

namespace Repository
{
    public class SampleRequiredRepo : RepoBase<SampleRequired>, ISampleRequiredRepo
    {
        public SampleRequiredRepo(RavenContext ctx) : base(ctx) { }

        public IEnumerable<SampleRequired> VerifySampleVLN(int parentMaterialNumber)
        {
            return FindByCondition(s => s.MaterialNumber == parentMaterialNumber)
                .ToList();
        }

    }
}
