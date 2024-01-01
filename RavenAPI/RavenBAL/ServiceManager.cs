using AutoMapper;
using Contracts;
using Service.Contracts;
using Service.src;


namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IMaterialServices> _materialServices;
        private readonly Lazy<IMaterialVendorServices> _vendorServices;
        public ServiceManager(IRepoManager repo, ILoggerManager log, IMapper mapper)
        {
            _materialServices = new Lazy<IMaterialServices>(() => new MaterialServices(repo,log,mapper));
            _vendorServices = new Lazy<IMaterialVendorServices>(() => new MaterialVendorServices(repo,log,mapper));
        }
        public IMaterialServices MaterialServices => _materialServices.Value;
        public IMaterialVendorServices VendorServices => _vendorServices.Value;
    }
}
