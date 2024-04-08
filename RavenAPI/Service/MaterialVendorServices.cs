using AutoMapper;
using Contracts;
using RavenDB.Exceptions;
using Service.Contracts;
using Service.Repo.Contracts;
using Shared.DTO;

namespace Service
{
    internal sealed class MaterialVendorServices : IMaterialVendorServices
    {
        private readonly IServiceRepoManager _serviceRepo;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public MaterialVendorServices(IServiceRepoManager serviceRepo, ILoggerManager logger, IMapper mapper)
        {
            _serviceRepo = serviceRepo;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<MaterialVendorDTO> GetMaterialVendorByMaterialNumber(int materialNumber)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<MaterialVendorWithRawMaterialDTO>> GetApprovedRawMaterial(int ParentMaterialNumber)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<RequiredSampleDTO>> InputRawMaterial(CreateRawMaterialDTO rawMaterial)
        {
            /*
             * Add/Update Vendor Lot
             * Check Required Sample
             * Add Drum --> per required sample
             * submit sample
             * update Drum Lot Number
             * add sample id to raw material and vendor lot
            */
            _serviceRepo.Vendor.InputVendorLot(rawMaterial);

            var materialVendor = await _serviceRepo.Vendor.GetMaterialVendor(rawMaterial.MaterialNumber)
                ?? throw new MaterialNotFoundException(rawMaterial.MaterialNumber);
           
            var vendorLot = materialVendor.VendorLots.FirstOrDefault(x => x.VendorLotNumber == rawMaterial.VendorLotNumber);

            var sample = await _serviceRepo.QualityControl.CheckRequiredSample(materialVendor);
            var requiredSample = sample.ToList();

            if(vendorLot.SampleSubmitNumber == null && requiredSample.Any())
            {

            }

            return new List<RequiredSampleDTO>();
        }
        public MaterialVendorDTO GetMaterialVendor(int materialNumber)
        {
            throw new NotImplementedException();
        }
    }
}
