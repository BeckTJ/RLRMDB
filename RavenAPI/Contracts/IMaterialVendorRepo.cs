using RavenDB.Models;

namespace Contracts
{
    public interface IMaterialVendorRepo:IRepoBase<MaterialVendor>
    {
        MaterialVendor GetMaterialVendor(int materialNumber);
        MaterialVendor GetMaterialVendorWithVendorLots(int materialNumber);
        IEnumerable<MaterialVendor> GetMaterialVendorFromParent(int parentMaterialNumber);
        IEnumerable<MaterialVendor> GetMaterialVendorWithVendorLot(int materialNumber);
    }
}
