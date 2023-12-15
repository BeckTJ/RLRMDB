
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
        public IEnumerable<MaterialVendorDTO> GetRawMaterialByMaterialNumber(int parentMaterialNumber)
        {
            var rawMaterial = _repo.RawMaterial.GetRawMaterialByMaterialNumber(parentMaterialNumber);

            if (rawMaterial == null)
                throw new MaterialNotFoundException(parentMaterialNumber);

            return _mapper.Map<IEnumerable<MaterialVendorDTO>>(rawMaterial);
        }
        //Still need to work on how material is sampled. 
        //Should sample be check by lot or individually
        public IEnumerable<MaterialVendorDTO> GetApprovedRawMaterial(int parentMaterialNumber)
        {
            var vendor = _repo.MaterialVendor.GetMaterialVendorWithVendorLot(parentMaterialNumber);

            if (vendor == null)
                throw new MaterialNotFoundException(parentMaterialNumber);

            var materialVendor = _mapper.Map<IEnumerable<MaterialVendorDTO>>(vendor);

            foreach (var material in materialVendor)
            {
                if (material.VendorLots != null)
                {
                    foreach (var lot in material.VendorLots)
                    {
                        var rawMaterial = _repo.RawMaterial.GetRawMaterialByMaterialNumber(material.MaterialNumber)
                            .Where(s => s.VendorLotNumber == lot.VendorLotNumber).ToList();
                        
                        var approved = VerifyRawMaterialSample(_mapper.Map<List<RawMaterialDTO>>(rawMaterial));
                        lot.RawMaterials = approved;
                    }
                }
            }
            return materialVendor;
        }
        public RawMaterialDTO GetRawMaterialByProductId(string productId)
        {
            var rawMaterial = _repo.RawMaterial.GetRawMaterialByProductId(productId); 
            return _mapper.Map<RawMaterialDTO>(rawMaterial);
        }

        private IEnumerable<RawMaterialDTO> VerifyRawMaterialSample(List<RawMaterialDTO> rawMaterial)
        {
            List<RawMaterialDTO> approved = new List<RawMaterialDTO>();

            foreach(var material in rawMaterial)
            {
                var sample = _repo.SampleRepo.VerifySample(material.SampleSubmitNumber);
                //create error check for not sampled
                if (sample.Approved && sample.ExperiationDate >= DateTime.Today)
                {
                    approved.Add(material);
                }
            }
            return approved;
        }

        /*
         * x4
         * Check sample required for material number -> 
         *     if only new required ->
         *          if previously sampled -> add qty to vendorbatch
         *              assign product id as material is used
         *          else ->display required sample (future create sample id)
         *      if new and old ->
         *          display sample required
         *              assign product id to all drums
        */

        public VendorLotDTO InputRawMaterial(CreateRawMaterialDTO rawMaterial)
        {
            SamplingServices sample = new(_repo, _logger, _mapper);
            MaterialVendorServices vendor = new(_repo,_logger,_mapper);
            List<RawMaterialDTO> rawMaterialDrum = new();

            var material = vendor.GetMaterialVendor(rawMaterial.MaterialNumber, rawMaterial.VendorName);
            
            rawMaterial.MaterialNumber = material.MaterialNumber;

            var vendorLot = vendor.VerifyMaterialVendorLot(rawMaterial);

            //

            var checkRequiredSample = _repo.SampleRequired.GetSampleRequired((int)material.ParentMaterialNumber)
                .Where(mt => mt.MaterialType.Equals("RawMaterial"));

            if(checkRequiredSample.Any(x => x.Vln == "New") && checkRequiredSample.Any(x => x.Vln == "Old"))
            {

                for (int i = 0; i < rawMaterial.Quantity; i++)
                {
                    sample.SubmitSample(rawMaterial.SampleSubmitNumber);

                    var drum = CreateRawMaterialDrum(rawMaterial);
                    rawMaterialDrum.Add(drum);

                    _repo.RawMaterial.CreateRawMaterial(_mapper.Map<RawMaterial>(rawMaterialDrum));
                }
                vendorLot.RawMaterials = rawMaterialDrum;
                _repo.Save();
                return vendorLot;
            }
            else
            {       //Check Sample
                var wasSampled = _repo.SampleRepo.VerifySample
                    (material.VendorLots.Where(x => x.VendorLotNumber == rawMaterial.VendorLotNumber)
                    .Select(x => x.SampleSubmitNumber).First());

                if(wasSampled != null)
                {       //Create Product Id
                    for (int i = 0; i <= rawMaterial.Quantity; i++)
                    {
                        var drum = CreateRawMaterialDrum(rawMaterial);
                        rawMaterialDrum.Add(drum);
                        _repo.RawMaterial.CreateRawMaterial(_mapper.Map<RawMaterial>(rawMaterialDrum));
                        _repo.Save();
                    }
                    vendorLot.RawMaterials = rawMaterialDrum;
                    return vendorLot;
                }
                else
                {       //Need to Sample and add to vendor lot
                    sample.SubmitSample(rawMaterial.SampleSubmitNumber);

                    for (int i = 0; i < rawMaterial.Quantity; i++)
                    {
                        var drum = CreateRawMaterialDrum(rawMaterial);
                        rawMaterialDrum.Add(drum);

                        _repo.RawMaterial.CreateRawMaterial(_mapper.Map<RawMaterial>(rawMaterialDrum));
                    }
                    _repo.Save();
                    vendorLot.RawMaterials = rawMaterialDrum;
                    return vendorLot;
                }
            }
        }

        /*
       * Create new raw material drum --> check via Vendor Material Number 
       * -> requires MaterialNumber, Vendor Lot Number, Sample Id
       * -> (MPPS) Inspection Lot Number, Drum Weight 
       * -> (When Required) Container Number, SAP Batch Number
       * -> return product
       */
        private RawMaterialDTO CreateRawMaterialDrum(CreateRawMaterialDTO rawMaterial)
        {
            ProductLotNumber productId = new ProductLotNumber(_repo);
            var material = _repo.MaterialVendor.GetMaterialVendor(rawMaterial.MaterialNumber);
            return new RawMaterialDTO
            {
                ProductId = productId.UpdateProductLotNumber(productId.CreateProductLotNumber(material)),
                BatchNumber = rawMaterial.BatchNumber,
                InspectionLotNumber = rawMaterial.InspectionLotNumber,
                ContainerNumber = rawMaterial.ContainerNumber,
                DrumWeight = rawMaterial.DrumWeight,
                VendorLotNumber = rawMaterial.VendorLotNumber,
                SampleSubmitNumber = rawMaterial.SampleSubmitNumber, 
            };
        }
    }
}
