using RavenDB.Models;

namespace Contracts
{
    public interface ISampleRequiredRepo : IRepoBase<SampleRequired>
    {
        Task<IEnumerable<SampleRequired>> GetSampleRequired(int parentMaterialNumber);
    }
}
