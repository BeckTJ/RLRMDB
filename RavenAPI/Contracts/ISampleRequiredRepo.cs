using RavenDB.Models;

namespace Contracts
{
    public interface ISampleRequiredRepo : IRepoBase<SampleRequired>
    {
        IEnumerable<SampleRequired> GetSampleRequired(int parentMaterialNumber);
    }
}
