using Contracts;
using Microsoft.EntityFrameworkCore;
using RavenDB.Data;
using RavenDB.Models;

namespace Repository.Async
{
    internal sealed class RawMaterialRepo : RepoBase<RawMaterial>, IRawMaterialRepo
    {
        public RawMaterialRepo(RavenContext ctx)
            : base(ctx)
        {
        }
        public void CreateRawMaterial(RawMaterial rawMaterial)
        {
            Create(rawMaterial);
        }
        public async Task<IEnumerable<RawMaterial>> GetAllRawMaterial() =>
             await FindAll()
                .OrderBy(rm => rm.MaterialNumber)
                .ToListAsync();
        public async Task<IEnumerable<RawMaterial>> GetRawMaterialByMaterialNumber(int materialNumber) =>
            await FindByCondition(rm => rm.MaterialNumber.Equals(materialNumber))
                .ToListAsync();
        public async Task<IEnumerable<RawMaterial>> GetRawMaterialByVendorLot(string vendorLot) =>
            await FindByCondition(rm => rm.VendorLotNumber == vendorLot).ToListAsync();
        public async Task<IEnumerable<RawMaterial>> GetRawMaterialWithSample(int materialNumber) =>
            await FindByCondition(rm => rm.Equals(materialNumber))
                .Include(s => s.Sample).ToListAsync();
        public async Task<RawMaterial> GetRawMaterialByProductId(string productId) =>
            await FindByCondition(x => x.DrumLotNumber.Equals(productId)).SingleOrDefaultAsync();
    }
}
