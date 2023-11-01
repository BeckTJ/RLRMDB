using Contracts;
using Microsoft.EntityFrameworkCore;
using RavenDAL.Data;
using RavenDAL.Models;

namespace Repository
{
    public class MaterialRepo : RepoBase<Material>, IMaterialRepo
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
            return FindByCondition(m => m.MaterialNumber == materialNumber)
                .FirstOrDefault();
        }
        public Material GetParentMaterialNumberFromChild(int materialNumber)
        {
            return FindByCondition(m => m.MaterialVendors.Equals(materialNumber))
                .FirstOrDefault();
        }

    }
}
