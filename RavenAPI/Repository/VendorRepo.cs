using Contracts;
using RavenDAL.Data;
using RavenDAL.Models;
using System.Data.Entity;
using System.Linq.Expressions;

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
            return FindByCondition(v => v.VendorBatchNumber == lotNumber)
                .FirstOrDefault();
        }

        public VendorBatch GetVendorWithRawMaterial(int materialNumber)
        {
            return FindByCondition(v => v.MaterialNumber.Equals(materialNumber))
                .Include(rm => rm.RawMaterials)
                .FirstOrDefault();
        }
    }
}
