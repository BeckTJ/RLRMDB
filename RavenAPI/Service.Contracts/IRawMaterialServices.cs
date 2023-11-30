using Shared.DTO;

namespace Service.Contracts
{
    public interface IRawMaterialServices
    {
        IEnumerable<RawMaterialDTO> GetAllRawMaterial();
        RawMaterialDTO InputRawMaterial(CreateRawMaterialDTO rawMaterial);
        IEnumerable<MaterialVendorDTO> GetApprovedRawMaterial(int parentMaterialNumber);

    }
}