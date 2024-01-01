using Contracts;
using Microsoft.EntityFrameworkCore;
using RavenDB.Data;
using RavenDB.Models;

namespace Repository
{
    internal sealed class MaterialVendorRepo:RepoBase<MaterialVendor>,IMaterialVendorRepo
    {
        public MaterialVendorRepo(RavenContext ctx) 
            : base(ctx){ }

        public MaterialVendor GetMaterialVendor(int materialNumber)
        {
            return FindByCondition(mv => mv.MaterialNumber == materialNumber)
                .FirstOrDefault();

        }
        public MaterialVendor GetMaterialVendorWithVendorLots(int materialNumber)
        {
            return FindByCondition(mv => mv.MaterialNumber == materialNumber)
                .Include(vl => vl.VendorLots)
                .FirstOrDefault();
        }
        public IEnumerable<MaterialVendor> GetMaterialVendorFromParent(int parentMaterialNumber) 
        {
            return FindByCondition(x => x.ParentMaterialNumber.Equals(parentMaterialNumber))
                .Include(v => v.VendorLots).ToList();
        }
        public IEnumerable<MaterialVendor> GetMaterialVendorWithVendorLot(int materialNumber)
        {
            return FindByCondition(mv => mv.ParentMaterialNumber.Equals(materialNumber))
                .Include(vl => vl.VendorLots)
                .ToList();
        }
    }
}
