using Contracts;
using Repository;
using Service.Contracts;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IRawMaterialService> _rawMaterialService;
        public ServiceManager(IRepoManager repo, ILoggerManager log)
        {
            _rawMaterialService = new Lazy<IRawMaterialService>(() => new RawMaterialServices(repo,log));
        }
        public IRawMaterialService RawMaterialService => _rawMaterialService.Value;
    }
}
