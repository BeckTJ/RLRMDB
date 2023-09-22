using Contracts;
using RavenDAL.Data;
using RavenDAL.Models;
using System.Linq.Expressions;

namespace Repository
{
    public class MaterialRepo : IRepoBase<Material>, IMaterialRepo
    {
        public MaterialRepo(RavenDBContext ctx) 
            : base(ctx) 
        {
        
        }
    }
}
