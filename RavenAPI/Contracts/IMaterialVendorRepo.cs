using RavenDB.Models;

namespace Contracts
{
    public interface IMaterialVendorRepo:IRepoBase<MaterialVendor>
    {
        MaterialVendor GetMaterialVendor(int materialNumber);
        IEnumerable<MaterialVendor> GetMaterialVendorsWithVendorLot(int materialNumber);
    }
}
