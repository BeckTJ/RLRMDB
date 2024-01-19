
namespace Service.Contracts
{
    public interface IServiceManager
    {
        IMaterialServices MaterialServices { get; }
        IMaterialVendorServices MaterialVendorServices { get; }
    }
}
