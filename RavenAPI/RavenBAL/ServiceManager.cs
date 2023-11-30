using AutoMapper;
using Contracts;
using Repository;
using Service.Contracts;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IRawMaterialServices> _rawMaterialService;
        private readonly Lazy<IMaterialServices> _materialServices;
        private readonly Lazy<ISamplingServices> _samplingServices;
        public ServiceManager(IRepoManager repo, ILoggerManager log, IMapper mapper)
        {
            _rawMaterialService = new Lazy<IRawMaterialServices>(() => new RawMaterialServices(repo,log,mapper));
            _materialServices = new Lazy<IMaterialServices>(() => new MaterialServices(repo,log,mapper));
            _samplingServices = new Lazy<ISamplingServices>(() => new SamplingServices(repo,log,mapper));

        }
        public IRawMaterialServices RawMaterialService => _rawMaterialService.Value;
        public IMaterialServices MaterialServices => _materialServices.Value;
        public ISamplingServices SamplingServices => _samplingServices.Value;
    }
}
