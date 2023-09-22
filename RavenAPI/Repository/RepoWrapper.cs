using Contracts;
using RavenDAL;
using RavenDAL.Data;

namespace Repository
{
    public class RepoWrapper : IRepoWrapper
    {
        private RavenDBContext _ctx;
        private IMaterialRepo _material;
        private IRawMaterialRepo _rawMaterial;

        public IMaterialRepo Material
        {
            get
            {
                if(_material == null)
                {
                    _material = new MaterialRepo(_ctx);
                }
                return _material;
            }
        }


        public IRawMaterialRepo RawMaterial
        {
            get
            {
                if (_rawMaterial == null)
                {
                    _rawMaterial = new RawMaterialRepo(_ctx);
                }
                return _rawMaterial;
            }
        }
        public RepoWrapper(RavenDBContext ctx)
        {
            _ctx = ctx;
        }


        public void Save()
        {
            _ctx.SaveChanges();
        }
    }
}
