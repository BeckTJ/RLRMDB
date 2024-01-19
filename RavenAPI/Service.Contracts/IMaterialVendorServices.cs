
using Shared.DTO;

namespace Service.Contracts
{
    public interface IMaterialVendorServices
    {
        MaterialVendorDTO GetMaterialVendor(int parentMaterialNumber, string vendorName);
        IEnumerable<MaterialVendorDTO> GetApprovedRawMaterial(int parentMaterialNumber);
        IEnumerable<MaterialVendorDTO> GetRawMaterialByMaterialNumber(int parentMaterialNumber);
        void InputRawMaterial(CreateRawMaterialDTO material);

    }
}
