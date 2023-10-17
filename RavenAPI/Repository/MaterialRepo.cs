using Contracts;
using Microsoft.EntityFrameworkCore;
using RavenDAL.Data;
using RavenDAL.Models;

namespace Repository
{
    public class MaterialRepo : RepoBase<Material>, IMaterialRepo
    {
        public MaterialRepo(RavenDBContext ctx) 
            : base(ctx) 
        {
        
        }
        public IEnumerable<Material> GetAllMaterial()
        {
            return FindAll()
                .OrderBy(m  => m.MaterialNumber)
                .ToList();
        }

        public Material GetMaterialByMaterialNumber(int materialNumber)
        {
            return FindByCondition(m => m.MaterialNumber == materialNumber)
                .Include(m => m.MaterialNumbers)
                .FirstOrDefault();
        }
        public Material GetParentMaterialNumberFromChild(int materialNumber)
        {
            return FindByCondition(m => m.MaterialNumbers.Equals(materialNumber))
                .FirstOrDefault();
        }

    }
}
