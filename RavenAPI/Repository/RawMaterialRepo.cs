using Contracts;
using Microsoft.EntityFrameworkCore;
using RavenDB.Data;
using RavenDB.Models;

namespace Repository
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
        public IEnumerable<RawMaterial> GetAllRawMaterial()
        {
            return FindAll()
                .OrderBy(rm => rm.MaterialNumber)
                .ToList();
        }
        public IEnumerable<RawMaterial> GetRawMaterialByMaterialNumber(int materialNumber)
        {
            return FindByCondition(rm => rm.MaterialNumber.Equals(materialNumber))
                .ToList();
        }
        public IEnumerable<RawMaterial> GetRawMaterialByVendorLot(string vendorLot)
        {
            return FindByCondition(rm => rm.VendorLotNumber == vendorLot).ToList();
        }
        public IEnumerable<RawMaterial> GetRawMaterialWithSample(int materialNumber)
        {
            return FindByCondition(rm => rm.Equals(materialNumber))
                .Include(s => s.Sample).ToList();
        }
        public RawMaterial GetRawMaterialByProductId(string  productId)
        {
            return FindByCondition(x => x.DrumLotNumber.Equals(productId)).FirstOrDefault();
        }
    }
}
