using Contracts;
using Microsoft.EntityFrameworkCore;
using RavenDB.Data;
using RavenDB.Models;

namespace Repository.Async
{
    internal sealed class VendorLotRepo : RepoBase<VendorLot>, IVendorRepo
    {
        public VendorLotRepo(RavenContext dbContext)
            : base(dbContext)
        {
        }
        public void SubmitVendorLot(VendorLot vendorLot)
        {
            Create(vendorLot);
        }
        public async Task<IEnumerable<VendorLot>> GetAllVendors() =>
            await FindAll()
                .OrderBy(v => v.MaterialNumber)
                .Include(rm => rm.RawMaterials)
                .ToListAsync();

        public async Task<VendorLot> GetVendorByVendorLot(string lotNumber) =>
            await FindByCondition(v => v.VendorLotNumber == lotNumber)
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<VendorLot>> GetVendorLotsWithRawMaterials(int materialNumber) =>
            await FindByCondition(v => v.MaterialNumber == materialNumber)
                .Include(v => v.RawMaterials)
                .ToListAsync();

        public void UpdateVendorLot(VendorLot vendorLot)
        {
            throw new NotImplementedException();
        }
    }
}
