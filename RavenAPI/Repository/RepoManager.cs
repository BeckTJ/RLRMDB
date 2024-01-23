using Contracts;
using RavenDB.Data;
using RavenDB.Models;
using Repository.Async;
using Service.Contracts;

namespace Repository
{
    public sealed class RepoManager : IRepoManager
    {
        private readonly RavenContext _ctx;
        private readonly Lazy<IMaterialRepo> _material;
        private readonly Lazy<IVendorRepo> _vendor;
        private readonly Lazy<IRawMaterialRepo> _rawMaterial;
        private readonly Lazy<ISampleRequiredRepo> _sampleRequired;
        private readonly Lazy<ISampleRepo> _sample;
        private readonly Lazy<IMaterialVendorRepo> _materialVendor;
        private readonly Lazy<IDateCode> _dateCode;

        public RepoManager(RavenContext ctx) 
        { 
            _ctx = ctx;
            _material = new Lazy<IMaterialRepo>(() => new MaterialRepo(_ctx));
            _vendor = new Lazy<IVendorRepo>(() => new VendorLotRepo(_ctx));    
            _rawMaterial = new Lazy<IRawMaterialRepo>(() => new RawMaterialRepo(_ctx));
            _sampleRequired = new Lazy<ISampleRequiredRepo>(() => new SampleRequiredRepo(_ctx));
            _sample = new Lazy<ISampleRepo>(() => new SampleRepo(_ctx));
            _materialVendor = new Lazy<IMaterialVendorRepo>(() => new MaterialVendorRepo(_ctx));
            _dateCode = new Lazy<IDateCode>(() => new DateCodeRepo(_ctx));
        }
        public IMaterialRepo Material => _material.Value;
        public IVendorRepo VendorLot => _vendor.Value;
        public IRawMaterialRepo RawMaterial => _rawMaterial.Value;
        public ISampleRequiredRepo SampleRequired => _sampleRequired.Value;
        public ISampleRepo SampleRepo => _sample.Value;
        public IMaterialVendorRepo MaterialVendor => _materialVendor.Value;
        public IDateCode DateCode => _dateCode.Value;

        public async Task Save() => await _ctx.SaveChangesAsync();
    }
}
