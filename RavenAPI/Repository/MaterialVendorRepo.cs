using Contracts;
using RavenDAL.Data;
using RavenDAL.Models;

namespace Repository
{
    public class MaterialVendorRepo:RepoBase<MaterialVendor>,IMaterialVendorRepo
    {
        public MaterialVendorRepo(RavenContext ctx) 
            : base(ctx){ }

        public MaterialVendor GetMaterialVendor(int materialNumber)
        {
            return FindByCondition(mv => mv.MaterialNumber == materialNumber)
                .FirstOrDefault();
        }
    }
}
