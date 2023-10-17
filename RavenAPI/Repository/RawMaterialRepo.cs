using Contracts;
using RavenDAL.Data;
using RavenDAL.Models;

namespace Repository
{
    public class RawMaterialRepo : RepoBase<RawMaterial>, IRawMaterialRepo
    {
        public RawMaterialRepo(RavenDBContext ctx) 
            : base(ctx) 
        { 
        }
        public void CreateRawMaterial(RawMaterial rawMaterial)
        {
            Create(rawMaterial);
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
        public IEnumerable<RawMaterial> GetRawMaterialByVendorLot(string vendorLot)
        {
            return FindByCondition(rm => rm.VendorLotNumber == vendorLot).ToList();
        }
    }
}
