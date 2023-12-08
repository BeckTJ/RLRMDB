using Shared.DTO;

namespace Service.Contracts
{
    public interface IRawMaterialServices
    {
        IEnumerable<RawMaterialDTO> GetAllRawMaterial();
        IEnumerable<RawMaterialDTO> InputRawMaterial(CreateRawMaterialDTO rawMaterial);
        IEnumerable<MaterialVendorDTO> GetApprovedRawMaterial(int parentMaterialNumber);
        IEnumerable<MaterialVendorDTO> GetRawMaterialByMaterialNumber(int parentMaterialNumber);
        RawMaterialDTO GetRawMaterialByProductId(string productId);
    }
}