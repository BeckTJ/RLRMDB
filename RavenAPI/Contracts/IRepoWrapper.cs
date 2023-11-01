
namespace Contracts
{
    public interface IRepoWrapper
    {
        IMaterialRepo Material { get; }
        IVendorRepo Vendor { get; }
        IRawMaterialRepo RawMaterial { get; }
        ISampleRequiredRepo SampleRequired { get; }
        ISampleRepo SampleRepo { get; } 
        IMaterialVendorRepo MaterialVendor { get; }
        void Save();
    }
}
