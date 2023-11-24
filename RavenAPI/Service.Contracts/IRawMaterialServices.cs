using Shared.DTO;

namespace Service.Contracts
{
    public interface IRawMaterialServices
    {
        RawMaterialDTO CreateRawMaterialDrum(CreateRawMaterialDTO material);
        RawMaterialDTO SampleRawMaterialDrum(RawMaterialDTO material);
        IEnumerable<RawMaterialDTO> ApprovedRawMaterial(int materialNumber);
    }
}