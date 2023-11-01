using Contracts;
using RavenDAL.Data;
using RavenDAL.Models;

namespace Repository
{
    public class RawMaterialRepo : RepoBase<RawMaterial>, IRawMaterialRepo
    {
        public RawMaterialRepo(RavenContext ctx) 
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
        public IEnumerable<RawMaterial> GetRawMaterialByMaterialNumber(int materialNumber)
        {
            return FindByCondition(rm => rm.MaterialNumber == materialNumber)
                .ToList();
        }
        public IEnumerable<RawMaterial> GetRawMaterialByVendorLot(string vendorLot)
        {
            return FindByCondition(rm => rm.VendorLotNumber == vendorLot).ToList();
        }
    }
}
