
using RavenDB.Models;
using Service.Contracts;

namespace Contracts
{
    public interface IRepoManager
    {
        IMaterialRepo Material { get; }
        IVendorRepo VendorLot { get; }
        IRawMaterialRepo RawMaterial { get; }
        ISampleRequiredRepo SampleRequired { get; }
        ISampleRepo SampleRepo { get; }
        IMaterialVendorRepo MaterialVendor { get; }
        IDateCode DateCode { get; }
        IProductLotNumber<MaterialVendor> RawMaterialLotNumber { get; }
        void Save();

    }
}
