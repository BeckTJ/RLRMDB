
using AutoMapper;
using Contracts;
using RavenDB.Exceptions;
using RavenDB.Models;
using Service.Contracts;
using Shared.DTO;

namespace Service
{
    internal sealed class RawMaterialServices : IRawMaterialServices
    {
        private readonly IRepoManager _repo;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public RawMaterialServices(IRepoManager repo, ILoggerManager log, IMapper mapper)
        {
            _repo = repo;
            _logger = log;
            _mapper = mapper;
        }

        public IEnumerable<RawMaterialDTO> GetAllRawMaterial()
        {
            var material = _repo.RawMaterial.GetAllRawMaterial();
            var rawMaterial = _mapper.Map<IEnumerable<RawMaterialDTO>>(material);
            return rawMaterial;
        }
        public IEnumerable<MaterialVendorDTO> GetApprovedRawMaterial(int parentMaterialNumber)
        {
            var vendorLot = _repo.MaterialVendor.GetMaterialVendorsWithVendorLot(parentMaterialNumber);

            if (vendorLot == null)
                throw new MaterialNotFoundException(parentMaterialNumber);

            var vendorLotDTO = _mapper.Map<IEnumerable<MaterialVendorDTO>>(vendorLot);

            foreach (var material in vendorLotDTO)
            {
                if (material.VendorLots != null)
                {
                    foreach (var lot in material.VendorLots)
                    {
                        lot.RawMaterials = ApprovedRawMaterial(lot.MaterialNumber);
                    }
                }
            }

            return vendorLotDTO;
        }
        public IEnumerable<RawMaterialDTO> ApprovedRawMaterial(int materialNumber)
        {
            var rawMaterial = _repo.RawMaterial.GetRawMaterialWithSample(materialNumber)
                .Where(s => s.Sample.Approved && s.Sample.ExperiationDate >= DateTime.Today);
            

            return _mapper.Map<IEnumerable<RawMaterialDTO>>(rawMaterial);
        }



        /*
         * Check sample required for material number -> 
         *     if only new required ->
         *          if previously sampled -> add qty to vendorbatch
         *              assign product id as material is used
         *          else ->display required sample (future create sample id)
         *      if new and old ->
         *          display sample required
         *              assign product id to all drums
        */

        public void InputRawMaterial(CreateRawMaterialDTO rawMaterial)
        {
            var materialVendor = _repo.MaterialVendor.GetMaterialVendor(rawMaterial.MaterialNumber);
        }

        /*
       * Create new raw material drum --> check via Vendor Material Number 
       * -> requires MaterialNumber, Vendor Lot Number, Sample Id
       * -> (MPPS) Inspection Lot Number, Drum Weight 
       * -> (When Required) Container Number, SAP Batch Number
       * -> return product
       */
        public RawMaterial CreateRawMaterialDrum(CreateRawMaterialDTO rawMaterial)
        {
            ProductLotNumber lot = new ProductLotNumber(_repo);
            var material = _repo.Material.GetMaterialByMaterialNumber(rawMaterial.MaterialNumber);
            RawMaterial raw = new()
            {
                ProductId = lot.UpdateProductLotNumber(lot.CreateProductLotNumber(material.MaterialVendors.First(x => x.MaterialNumber == rawMaterial.MaterialNumber))),
                MaterialNumber = rawMaterial.MaterialNumber,
                VendorLotNumber = rawMaterial.VendorLotNumber,
                SapBatchNumber = rawMaterial.BatchNumber,
                ContainerNumber = rawMaterial.ContainerNumber,
                InspectionLotNumber = rawMaterial.InspectionLotNumber,
                SampleSubmitNumber = rawMaterial.SampleId,
            };

            _repo.RawMaterial.CreateRawMaterial(raw);

            return raw;
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
        private IEnumerable<RawMaterial> ExpiredRawMaterial(int materialNumber)
        {
            return _repo.RawMaterial.GetRawMaterialWithSample(materialNumber)
                .Where(s => s.Sample.ExperiationDate < DateTime.Today);
        }
        private IEnumerable<RawMaterial> RawMaterialAwaitingApproval(int materialNumber)
        {
            return _repo.RawMaterial.GetRawMaterialWithSample(materialNumber)
                .Where(s => !s.Sample.Approved && !s.Sample.Rejected);
        }
        private IEnumerable<RawMaterial> RejectedRawMaterial(int materialNumber)
        {
            return _repo.RawMaterial.GetRawMaterialWithSample(materialNumber)
                .Where(s => s.Sample.Rejected);

        }

        RawMaterialDTO IRawMaterialServices.CreateRawMaterialDrum(CreateRawMaterialDTO material)
        {
            throw new NotImplementedException();
        }
    }
}
