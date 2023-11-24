using AutoMapper;
using Contracts;
using Repository;
using Service.Contracts;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IRawMaterialServices> _rawMaterialService;
        private readonly Lazy<MaterialServices> _materialServices;
        public ServiceManager(IRepoManager repo, ILoggerManager log, IMapper mapper)
        {
            _rawMaterialService = new Lazy<IRawMaterialServices>(() => new RawMaterialServices(repo,log,mapper));
            _materialServices = new Lazy<MaterialServices>(() => new MaterialServices(repo,log,mapper));

        }
        public IRawMaterialServices RawMaterialService => _rawMaterialService.Value;
        public IMaterialServices MaterialServices => _materialServices.Value;
    }
}
