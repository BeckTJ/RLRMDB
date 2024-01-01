
using Shared.DTO;

namespace Service.Contracts
{
    public interface IMaterialVendorServices
    {
        public MaterialVendorDTO GetMaterialVendor(int parentMaterialNumber, string vendorName);
        public IEnumerable<MaterialVendorDTO> GetApprovedRawMaterial(int parentMaterialNumber);
        public IEnumerable<MaterialVendorDTO> GetRawMaterialByMaterialNumber(int parentMaterialNumber);
    }
}
