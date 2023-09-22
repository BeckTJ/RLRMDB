using Contracts;
using RavenDAL.Data;
using RavenDAL.Models;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

namespace Repository
{
    public class RawMaterialRepo : RepoBase<RawMaterial>, IRawMaterialRepo
    {
        public RawMaterialRepo(RavenDBContext ctx) 
            : base(ctx) 
        { 
        }

        public IEnumerable<RawMaterial> GetAllRawMaterial()
        {
            return FindAll()
                .OrderBy(rm => rm.MaterialNumber)
                .ToList();
        }

        public RawMaterial GetRawMaterialByMaterialNumber(int materialNumber)
        {
            return FindByCondition(rm => rm.MaterialNumber == materialNumber)
                .FirstOrDefault();
        }
    }
}
