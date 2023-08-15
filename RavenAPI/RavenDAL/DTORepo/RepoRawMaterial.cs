using RavenDAL.Data;
using RavenDAL.DTO;
using RavenDAL.Interface;
using RavenDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDAL.DTORepo
{
    public class RepoRawMaterial : IRawMaterial<RawMaterialDTO>
    {
        private readonly RavenDBContext _ctx;
        private readonly IRepository<RawMaterial> _rawMaterial;
        public RepoRawMaterial(RavenDBContext ctx, IRepository<RawMaterial> rawMaterial)
        {
            _ctx = ctx;
            _rawMaterial = rawMaterial;
        }
        public void Create(RawMaterialDTO _object)
        {
            var raw = _rawMaterial.Create( new RawMaterial
            {
                DrumLotNumber = _object.ProductId,
                SapBatchNumber = _object.BatchNumber,
                ContainerNumber = _object.ContainerNumber,
                VendorBatchNumber = _object.VendorLotNumber,
            });
        }
        public void Update(RawMaterialDTO _object)
        {
            var raw = _ctx.RawMaterials.First(x => x.DrumLotNumber == _object.ProductId);

            var material = new RawMaterial
            {
                DrumLotNumber = _object.ProductId,
                ContainerNumber = _object.ContainerNumber,
                SapBatchNumber = _object.BatchNumber,
                VendorBatchNumber = _object.VendorLotNumber,
                DrumWeight = _object.DrumWeight,
                SampleSubmitNumber = _object.SampleId,
                InspectionLotNumber = _object.InspectionLotNumber,
            };

            _ctx.RawMaterials.Update(material);
            _ctx.SaveChanges();
        }
        public void Delete(RawMaterialDTO _object)
        {
            throw new NotImplementedException();
        }
        public RawMaterialDTO GetByProductId(string productId)
        {
            return _ctx.RawMaterials.Select(x => new RawMaterialDTO
            {
                MaterialNumber = (int)x.MaterialNumber,
                ProductId = x.DrumLotNumber,
                BatchNumber = (int)x.SapBatchNumber,
                ContainerNumber = x.ContainerNumber,
                VendorLotNumber = x.VendorBatchNumber,
                DrumWeight = x.DrumWeight,
            }).FirstOrDefault(x => x.ProductId == productId);
        }

        public IEnumerable<RawMaterialDTO> GetAll()
        {
            return _ctx.RawMaterials.Select(x => new RawMaterialDTO
            {
                MaterialNumber = (int)x.MaterialNumber,
                ProductId = x.DrumLotNumber,
                BatchNumber = (int)x.SapBatchNumber,
                ContainerNumber = x.ContainerNumber,
                VendorLotNumber = x.VendorBatchNumber,
                DrumWeight = x.DrumWeight,
            }).ToList();
        }
        public IEnumerable<RawMaterialDTO> GetAllByMaterialNumber(int materialNumber)
        {
            return _ctx.RawMaterials
                .Where(x => x.MaterialNumber == materialNumber).Select(x => new RawMaterialDTO
                {
                    MaterialNumber = (int)x.MaterialNumber,
                    ProductId = x.DrumLotNumber,
                    BatchNumber = (int)x.SapBatchNumber,
                    ContainerNumber = x.ContainerNumber,
                    VendorLotNumber = x.VendorBatchNumber,
                    DrumWeight = x.DrumWeight,
                }).ToList();
        }
        public IEnumerable<RawMaterialDTO> GetAllByVendorLotNumber(string vendorLotNumber)
        {
            return _ctx.RawMaterials
                .Where(r => r.VendorBatchNumber == vendorLotNumber)
                .Select(r => new RawMaterialDTO
                {
                    MaterialNumber = (int)r.MaterialNumber,
                    ProductId = r.DrumLotNumber,
                    BatchNumber = (int)r.SapBatchNumber,
                    ContainerNumber = r.ContainerNumber,
                    VendorLotNumber = r.VendorBatchNumber,
                    DrumWeight = r.DrumWeight,
                });
        }

        Task<RawMaterialDTO> IRawMaterial<RawMaterialDTO>.Create(RawMaterialDTO _object)
        {
            throw new NotImplementedException();
        }
    }
}
