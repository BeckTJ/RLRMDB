using Contracts;
using RavenDB.Data;
using RavenDB.Models;

namespace Repository
{
    public class SampleRequiredRepo : RepoBase<SampleRequired>, ISampleRequiredRepo
    {
        public SampleRequiredRepo(RavenContext ctx) : base(ctx) { }

        public IEnumerable<SampleRequired> GetSampleRequired(int parentMaterialNumber)
        {
            return FindByCondition(s => s.MaterialNumber.Equals(parentMaterialNumber));
        }
    }
}
