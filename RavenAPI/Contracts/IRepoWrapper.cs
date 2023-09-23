
namespace Contracts
{
    public interface IRepoWrapper
    {
        IMaterialRepo Material { get; }
        IRawMaterialRepo RawMaterial { get; }
        IVendorRepo Vendor { get; }
        void Save();
    }
}
