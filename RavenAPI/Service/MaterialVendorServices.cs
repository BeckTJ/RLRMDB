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

        public async Task<IEnumerable<RequliredSampleDTO>> InputRawMaterial(CreateRawMaterialDTO rawMaterial)
        {
            /*
             * Add/Update Vendor Lot
             * Check Required Sample
             * Add Drum --> per required sample
             * submit sample
             * update Drum Lot Number
             * add sample id to raw material and vendor lot
            */
            var materialVendor = await _serviceRepo.Vendor.GetMaterialVendor(rawMaterial.MaterialNumber)
                ?? throw new MaterialNotFoundException(rawMaterial.MaterialNumber);

            var requiredSample = _serviceRepo.QualityControl.CheckRequiredSample(materialVendor);

            /*Input Vendor Lot
             * Verify if lot is in db
             * if yes increase qty
             * else input vendorlot
             */
            _serviceRepo.Vendor.InputVendorLot(rawMaterial);

            for (int i = 0; i < rawMaterial.Quantity; i++)
            {

                //Input Sample
                _serviceRepo.QualityControl.SubmitSample("",0);
                //Input Drum
                _serviceRepo.RawMaterialDrum.CreateRawMaterialDrum(rawMaterial);
            }
            return new List<RequliredSampleDTO>();
        }
        public MaterialVendorDTO GetMaterialVendor(int materialNumber)
        {
            throw new NotImplementedException();
        }
    }
}
