using Contracts;
using Microsoft.EntityFrameworkCore;
using RavenDAL.Data;
using RavenDAL.Models;

namespace Repository
{
    public class VendorRepo : RepoBase<VendorBatch>, IVendorRepo
    {
        public VendorRepo(RavenDBContext dbContext) 
            : base(dbContext) 
        {
            
        }

        public IEnumerable<VendorBatch> GetAllVendors()
        {
            return FindAll()
                .OrderBy(v => v.MaterialNumber)
                .ToList();
        }

        public VendorBatch GetVendorByVendorLot(string lotNumber)
        {
            return FindByCondition(v => v.VendorLotNumber == lotNumber)
                .FirstOrDefault();
        }

        public IEnumerable<VendorBatch> GetVendorLotsWithRawMaterials(int materialNumber)
        {
            return FindByCondition(v => v.MaterialNumber == materialNumber)
                .Include(v => v.RawMaterials)
                .ToList();
        }
    }
}
