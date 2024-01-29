using Shared.DTO;

namespace Service.Contracts
{
    public interface IMaterialVendorServices
    {
        Task<MaterialVendorDTO> GetMaterialVendor(int materialNumber);
        Task<IEnumerable<MaterialVendorWithRawMaterialDTO>> GetApprovedRawMaterial(int materialNumber);
        Task InputRawMaterial(CreateRawMaterialDTO material);
    }
}
