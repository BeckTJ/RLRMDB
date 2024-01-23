using RavenDB.Models;

namespace Contracts
{
    public interface IMaterialVendorRepo:IRepoBase<MaterialVendor>
    {
        Task<MaterialVendor> GetMaterialVendor(int materialNumber);
        Task<MaterialVendor> GetMaterialVendorWithVendorLots(int materialNumber);
        Task<IEnumerable<MaterialVendor>> GetMaterialVendorFromParent(int parentMaterialNumber);
        Task<IEnumerable<MaterialVendor>> GetAllMaterialVendorWithVendorLot(int materialNumber);
        Task<IEnumerable<MaterialVendor>> GetMaterialVendorWithRawMaterial(int materialNumber);
    }
}
