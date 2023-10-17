using RavenDAL.Models;

namespace Contracts
{
    public interface ISampleRepo : IRepoBase<SampleSubmit>
    {
        SampleSubmit VerifySample(string lotNumber);

    }
}
