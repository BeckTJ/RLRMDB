using Contracts;
using RavenDAL.Data;
using RavenDAL.Models;
using System.Linq.Expressions;

namespace Repository
{
    public class RawMaterialRepo : IRepoBase<RawMaterial>, IRawMaterialRepo
    {
        public RawMaterialRepo(RavenDBContext ctx) 
            : base(ctx) 
        { 
        }
    }
}
