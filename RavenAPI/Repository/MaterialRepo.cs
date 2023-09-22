using Contracts;
using RavenDAL.Data;
using RavenDAL.Models;
using System.Data.Entity;
using System.Linq.Expressions;

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
                .FirstOrDefault();
        }

    }
}
