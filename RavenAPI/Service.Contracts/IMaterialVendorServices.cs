using Shared.DTO;

namespace Service.Contracts
{
    public interface IMaterialVendorServices
    {
        Task<MaterialVendorWithVendorLotDTO> GetMaterialVendor(int materialNumber);
        Task<IEnumerable<MaterialVendorWithRawMaterialDTO>> GetApprovedRawMaterial(int materialNumber);
        Task InputRawMaterial(CreateRawMaterialDTO material);
    }
}
