using Contracts;
using RavenDAL;
using RavenDAL.Data;

namespace Repository
{
    public class RepoWrapper : IRepoWrapper
    {
        private RavenContext _ctx;
        private IMaterialRepo _material;
        private IVendorRepo _vendor;
        private IRawMaterialRepo _rawMaterial;
        private ISampleRequiredRepo _sampleRequired;
        private ISampleRepo _sample;
        private IMaterialVendorRepo _materialVendor;

        public IMaterialRepo Material
        {
            get
            {
                _material ??= new MaterialRepo(_ctx);
                return _material;
            }
        }

        public IRawMaterialRepo RawMaterial
        {
            get
            {
                _rawMaterial ??= new RawMaterialRepo(_ctx);
                return _rawMaterial;
            }
        }
        public IVendorRepo Vendor
        {
            get
            {
                _vendor ??= new VendorRepo(_ctx);
                return _vendor;
            }
        }
 
        public ISampleRequiredRepo SampleRequired
        {
            get
            {
                _sampleRequired ??= new SampleRequiredRepo(_ctx);
                return _sampleRequired;
            }
        }

        public ISampleRepo SampleRepo
        {
            get
            {
                _sample ??= new SampleRepo(_ctx);
                return _sample;
            }
        }

        public IMaterialVendorRepo MaterialVendor
        {
            get 
            {
                _materialVendor ??= new MaterialVendorRepo(_ctx);
                return _materialVendor; 
            }
        }

        public RepoWrapper(RavenContext ctx)
        {
            _ctx = ctx;
        }

        public void Save()
        {
            _ctx.SaveChanges();
        }
    }
}
