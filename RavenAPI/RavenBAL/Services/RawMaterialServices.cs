
using AutoMapper;
using Contracts;
using RavenBAL.Interface;
using RavenDAL.DTO;
using RavenDAL.Models;

namespace RavenBAL.Services
{
    public class RawMaterialServices : IRawMaterialService
    {
        private IRepoWrapper _repo;

        public RawMaterialServices(IRepoWrapper repo) 
        {
            _repo = repo;
        }

        /*
       * Create new raw material drum --> check via Vendor Material Number 
       * -> requires MaterialNumber, Vendor Lot Number, Sample Id
       * -> (MPPS) Inspection Lot Number, Drum Weight 
       * -> (When Required) Container Number, SAP Batch Number
       * -> return product id 
       */
        public RawMaterial CreateRawMaterialDrum(CreateRawMaterialDTO rawMaterial)
        {
            ProductLotNumber lot = new ProductLotNumber(_repo);
            var material = _repo.Material.GetMaterialByMaterialNumber(rawMaterial.MaterialNumber);
            RawMaterial raw = new()
            {
                ProductId = lot.CreateProductLotNumber(material.MaterialVendors.First(x => x.MaterialNumber == rawMaterial.MaterialNumber)),
                MaterialNumber = rawMaterial.MaterialNumber,
                VendorLotNumber = rawMaterial.VendorLotNumber,
                SapBatchNumber = rawMaterial.BatchNumber,
                ContainerNumber = rawMaterial.ContainerNumber,
                InspectionLotNumber = rawMaterial.InspectionLotNumber,
                SampleSubmitNumber = rawMaterial.SampleId,
             };

            _repo.RawMaterial.CreateRawMaterial(raw);

            return  raw;
        }
        private string GetProductLotNumber(int materialNumber)
        {
            var material = _repo.MaterialVendor.GetMaterialVendor(materialNumber);
            var rawMaterial = _repo.RawMaterial.GetRawMaterialByMaterialNumber(materialNumber).OrderByDescending(rm => rm.ProductId).FirstOrDefault();
            int id;
            if (rawMaterial != null)
            {
                if(rawMaterial.ProductId.Length == 10 ||  rawMaterial.ProductId.Length == 6)
                {
                    id = int.Parse(rawMaterial.ProductId[..3]) + 1;
                }
                else
                {
                    id = int.Parse(rawMaterial.ProductId[..2]) + 1;
                }
                return id + material.MaterialCode;
            }
            else
            {
                return material.SequenceId + material.MaterialCode;
            }
        }
        public RawMaterialDTO SampleRawMaterialDrum(RawMaterialDTO material)
        {
            throw new NotImplementedException();
        }

        /*
        * Get Raw Material
        * -> return List of RawMaterialDTO
        * -> Lot has been sampled
        * -> check Sample Approved/Not Expired
        */
        public IEnumerable<RawMaterialDTO> ApprovedRawMaterial(int materialNumber)
        {
            throw new NotImplementedException();
        }
    }
}
