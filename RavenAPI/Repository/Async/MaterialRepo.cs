using Contracts;
using Microsoft.EntityFrameworkCore;
using RavenDB.Data;
using RavenDB.Models;

namespace Repository.Async
{
    internal sealed class MaterialRepo : RepoBase<Material>, IMaterialRepo
    {
        public MaterialRepo(RavenContext ctx)
            : base(ctx)
        {
        }
        public async Task<IEnumerable<Material>> GetAllMaterial() =>
            await FindAll()
                .OrderBy(m => m.MaterialNumber)
                .Include(m => m.MaterialVendors)
                .ToListAsync();

        public async Task<Material> GetMaterialByMaterialNumber(int materialNumber) =>
            await FindByCondition(m => m.MaterialNumber.Equals(materialNumber))
                .Include(mv => mv.MaterialVendors)
                .SingleOrDefaultAsync();

        public async Task<Material> GetParentMaterialNumberForChild(int materialNumber) =>
            await FindByCondition(m => m.MaterialVendors.Equals(materialNumber))
                .SingleOrDefaultAsync();


    }
}
