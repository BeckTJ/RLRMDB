using RavenDAL.Data;
using RavenDAL.DTO;
using RavenDAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDAL.DTORepo
{
    public class RepoRawMaterialDrum : IRawMaterialDrum<RawMaterialDrumDTO>
    {
        private readonly RavenDBContext _ctx;
        public RepoRawMaterialDrum(RavenDBContext ctx)
        {
            _ctx = ctx;
        }   
        public IEnumerable<RawMaterialDrumDTO> GetAll()
        {
            return _ctx.RawMaterials.Select(x => new RawMaterialDrumDTO
            {
                MaterialNumber = x.MaterialNumber,
                DrumLotNumber = x.DrumLotNumber,
                DrumBatchNumber = x.SapBatchNumber,
                ContainerNumber = x.ContainerNumber,
                VendorLotNumber = x.VendorBatchNumber,
                DrumWeight = x.DrumWeight,
            }).ToList();
        }
        public IEnumerable<RawMaterialDrumDTO> GetAllByMaterialNumber(int materialNumber) 
        {
            return _ctx.RawMaterials
                .Where(x => x.MaterialNumber == materialNumber).Select(x => new RawMaterialDrumDTO
            {
                MaterialNumber = x.MaterialNumber,
                DrumLotNumber = x.DrumLotNumber,
                DrumBatchNumber = x.SapBatchNumber,
                ContainerNumber = x.ContainerNumber,
                VendorLotNumber = x.VendorBatchNumber,
                DrumWeight = x.DrumWeight,
            }).ToList();
        }
        public RawMaterialDrumDTO GetByProductId(string productId)
        {
            return _ctx.RawMaterials.Select(x => new RawMaterialDrumDTO
            {
                MaterialNumber = x.MaterialNumber,
                DrumLotNumber = x.DrumLotNumber,
                DrumBatchNumber = x.SapBatchNumber,
                ContainerNumber = x.ContainerNumber,
                VendorLotNumber = x.VendorBatchNumber,
                DrumWeight = x.DrumWeight,
            }).FirstOrDefault(x => x.DrumLotNumber == productId);
        }
        public void Update(RawMaterialDrumDTO _object)
        {
            throw new NotImplementedException();
        }

        public Task<RawMaterialDrumDTO> Create(RawMaterialDrumDTO _object)
        {
            _ = _ctx.RawMaterials.Add(new Models.RawMaterial
            {
                DrumLotNumber = _object.DrumLotNumber,
                SapBatchNumber = _object.DrumBatchNumber,
                ContainerNumber = _object.ContainerNumber,
                VendorBatchNumber = _object.VendorLotNumber,
            });
            _ctx.SaveChanges();

            throw new NotImplementedException();

            //return;
        }

        public void Delete(RawMaterialDrumDTO _object)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RawMaterialDrumDTO> GetAllByVendorLotNumber(string vendorLotNumber)
        {
            return _ctx.RawMaterials
                .Where(r => r.VendorBatchNumber == vendorLotNumber)
                .Select(r => new RawMaterialDrumDTO
                {
                    MaterialNumber = r.MaterialNumber,
                    DrumLotNumber = r.DrumLotNumber,
                    DrumBatchNumber = r.SapBatchNumber,
                    ContainerNumber = r.ContainerNumber,
                    VendorLotNumber = r.VendorBatchNumber,
                    DrumWeight = r.DrumWeight,
                });
        }
    }
}
