using RavenDAL.Models;

namespace Contracts
{
    public interface ISampleRequiredRepo : IRepoBase<SampleRequired>
    {
        IEnumerable<SampleRequired> VerifySampleVLN(int parentMaterialNumber);
    }
}
