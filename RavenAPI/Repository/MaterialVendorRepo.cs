using Contracts;
using Microsoft.EntityFrameworkCore;
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
        public IEnumerable<MaterialVendor> GetMaterialVendorsWithVendorLot(int materialNumber)
        {
            return FindByCondition(mv => mv.MaterialNumber.Equals(materialNumber))
                .Include(vl => vl.VendorLots)
                .ToList();
        }
    }
}
