using Service.Repo.Contracts;
using Contracts;
using AutoMapper;

namespace Service.Repo
{
    public sealed class ServiceRepoManager : IServiceRepoManager
    {
        private readonly Lazy<IRawMaterialDrum> _rawMaterialDrum;
        private readonly Lazy<IHighPurityMaterial> _highPurityMaterial;
        private readonly Lazy<IVendor> _vendor;
        private readonly Lazy<IQualityControl> _qualityControl;

        public ServiceRepoManager(IRepoManager repo, ILoggerManager logger, IMapper mapper) 
        {
            _rawMaterialDrum = new Lazy<IRawMaterialDrum>(() => new RawMaterialDrum(repo,logger,mapper));
            _highPurityMaterial = new Lazy<IHighPurityMaterial>(() => new HighPurityMaterial(repo, logger, mapper));
            _vendor = new Lazy<IVendor>(() => new Vendor(repo, logger, mapper));
            _qualityControl = new Lazy<IQualityControl>(() => new QualityControl(repo, logger, mapper));
        }
        public IRawMaterialDrum RawMaterialDrum => _rawMaterialDrum.Value;
        public IHighPurityMaterial HighPurityMaterial => _highPurityMaterial.Value;
        public IVendor Vendor => _vendor.Value;

        public IQualityControl QualityControl => _qualityControl.Value;
    }
}
