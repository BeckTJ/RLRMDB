using Shared.DTO;

namespace Service.Contracts
{
    public interface IMaterialVendorServices
    {
        Task<MaterialVendorDTO> GetMaterialVendor(int materialNumber);
        Task<IEnumerable<MaterialVendorDTO>> GetApprovedRawMaterial(int parentMaterialNumber);
        Task InputRawMaterial(CreateRawMaterialDTO material);
    }
}
