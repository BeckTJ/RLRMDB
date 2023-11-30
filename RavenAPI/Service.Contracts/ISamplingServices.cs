
using Shared.DTO;

namespace Service.Contracts
{
    public interface ISamplingServices
    {
        IEnumerable<RequiredSampleDTO> VerifySampleRequired(int parentMaterialNumber);
    }
}
