using AutoMapper;
using Contracts;
using Service.Contracts;
using Service.src;


namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IMaterialServices> _materialServices;
        private readonly Lazy<IMaterialVendorServices> _materialVendorServices;
        public ServiceManager(IRepoManager repo, ILoggerManager log, IMapper mapper)
        {
            _materialServices = new Lazy<IMaterialServices>(() => new MaterialServices(repo,log,mapper));
            _materialVendorServices = new Lazy<IMaterialVendorServices>(() => new MaterialVendorServices(repo,log,mapper));
        }
        public IMaterialServices MaterialServices => _materialServices.Value;
        public IMaterialVendorServices MaterialVendorServices => _materialVendorServices.Value;
    }
}
