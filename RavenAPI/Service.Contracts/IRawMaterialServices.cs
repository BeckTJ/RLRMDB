using Shared.DTO;

namespace Service.Contracts
{
    public interface IRawMaterialServices
    {
        IEnumerable<RawMaterialDTO> GetAllRawMaterial();
        RawMaterialDTO CreateRawMaterialDrum(CreateRawMaterialDTO material);
        RawMaterialDTO SampleRawMaterialDrum(RawMaterialDTO material);
        IEnumerable<MaterialVendorDTO> GetApprovedRawMaterial(int parentMaterialNumber);

    }
}