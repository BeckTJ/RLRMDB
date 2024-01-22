using Shared.DTO;

namespace Service.Contracts
{
    public interface IMaterialVendorServices
    {
        MaterialVendorDTO GetMaterialVendor(int materialNumber);
        IEnumerable<MaterialVendorDTO> GetApprovedRawMaterial(int parentMaterialNumber);
        void InputRawMaterial(CreateRawMaterialDTO material);
    }
}
