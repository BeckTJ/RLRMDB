using Contracts;
using Microsoft.EntityFrameworkCore;
using RavenDB.Data;
using RavenDB.Models;

namespace Repository
{
    internal sealed class MaterialRepo : RepoBase<Material>, IMaterialRepo
    {
        public MaterialRepo(RavenContext ctx) 
            : base(ctx) 
        {
        
        }
        public IEnumerable<Material> GetAllMaterial()
        {
            return FindAll()
                .OrderBy(m  => m.MaterialNumber)
                .Include(m => m.MaterialVendors)
                .ToList();
        }

        public Material GetMaterialByMaterialNumber(int materialNumber)
        {
            return FindByCondition(m => m.MaterialNumber.Equals(materialNumber))
                .Include(mv => mv.MaterialVendors)
                .FirstOrDefault();
        }
        public Material GetParentMaterialNumberForChild(int materialNumber)
        {
            return FindByCondition(m => m.MaterialVendors.Equals(materialNumber))
                .FirstOrDefault();
        }

    }
}
