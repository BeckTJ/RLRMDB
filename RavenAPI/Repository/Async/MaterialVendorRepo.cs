using Contracts;
using Microsoft.EntityFrameworkCore;
using RavenDB.Data;
using RavenDB.Models;

namespace Repository.Async
{
    internal sealed class MaterialVendorRepo : RepoBase<MaterialVendor>, IMaterialVendorRepo
    {
        public MaterialVendorRepo(RavenContext ctx)
            : base(ctx) { }

        public async Task<MaterialVendor> GetMaterialVendor(int materialNumber) =>
            await FindByCondition(mv => mv.MaterialNumber == materialNumber)
                .FirstOrDefaultAsync();

        public async Task<MaterialVendor> GetMaterialVendorWithVendorLots(int materialNumber) =>
            await FindByCondition(mv => mv.MaterialNumber == materialNumber)
                .Include(vl => vl.VendorLots)
                .SingleOrDefaultAsync();

        public async Task<IEnumerable<MaterialVendor>> GetMaterialVendorFromParent(int parentMaterialNumber) =>
            await FindByCondition(x => x.ParentMaterialNumber.Equals(parentMaterialNumber))
                .Include(v => v.VendorLots).ToListAsync();

        public async Task<IEnumerable<MaterialVendor>> GetAllMaterialVendorWithVendorLot(int materialNumber) =>
            await FindByCondition(mv => mv.MaterialNumber.Equals(materialNumber))
                .Include(vl => vl.VendorLots)
                .ToListAsync();

        public async Task<IEnumerable<MaterialVendor>> GetMaterialVendorWithRawMaterial(int ParentMaterialNumber) =>
            await FindByCondition(mv => mv.ParentMaterialNumber.Equals(ParentMaterialNumber))
            .Include(rm => rm.RawMaterials).ToListAsync();

    }
}
