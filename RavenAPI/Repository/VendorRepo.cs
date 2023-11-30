using Contracts;
using Microsoft.EntityFrameworkCore;
using RavenDB.Data;
using RavenDB.Models;

namespace Repository
{
    public class VendorRepo : RepoBase<VendorLot>, IVendorRepo
    {
        public VendorRepo(RavenContext dbContext) 
            : base(dbContext) 
        {
            
        }
        public void SubmitVendorLot(VendorLot vendorLot)
        {
            Create(vendorLot);
        }
        public IEnumerable<VendorLot> GetAllVendors()
        {
            return FindAll()
                .OrderBy(v => v.MaterialNumber)
                .Include(rm => rm.RawMaterials)
                .ToList();
        }

        public VendorLot GetVendorByVendorLot(string lotNumber)
        {
            return FindByCondition(v => v.VendorLotNumber == lotNumber)
                .FirstOrDefault();
        }

        public IEnumerable<VendorLot> GetVendorLotsWithRawMaterials(int materialNumber)
        {
            return FindByCondition(v => v.MaterialNumber == materialNumber)
                .Include(v => v.RawMaterials)
                .ToList();
        }
    }
}
