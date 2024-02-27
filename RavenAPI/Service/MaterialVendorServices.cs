using AutoMapper;
using Contracts;
using RavenDB.Exceptions;
using RavenDB.Models;
using Service.Contracts;
using Service.Repo.Contracts;
using Shared.DTO;

namespace Service
{
    internal sealed class MaterialVendorServices : IMaterialVendorServices
    {
        private readonly IServiceRepoManager _repo;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public MaterialVendorServices(IServiceRepoManager repo, ILoggerManager logger, IMapper mapper)
        {
            _repo = repo;
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
        /*
         * need to verify if vendor lot is in db
         * check what samples are required
         * verify what drums need to be sampled
        */
        public async Task<IEnumerable<RequiredSampleDTO>> InputRawMaterial(CreateRawMaterialDTO rawMaterial)
        {
            var materialVendor = await _repo.Vendor.GetMaterialVendor(rawMaterial.MaterialNumber)
                ?? throw new MaterialNotFoundException(rawMaterial.MaterialNumber);

            /*Input Vendor Lot
             * Verify if lot is in db
             * if yes increase qty
             * else input vendorlot
             */
            _repo.Vendor.InputVendorLot(rawMaterial);

            for (int i = 0; i < rawMaterial.Quantity; i++)
            {
                //Input Sample
                _repo.QualityControl.SubmitSample("",0);
                //Input Drum
                _repo.RawMaterialDrum.CreateRawMaterialDrum(rawMaterial);
            }
            return new List<RequiredSampleDTO>();
        }
        public MaterialVendorDTO GetMaterialVendor(int materialNumber)
        {
            throw new NotImplementedException();
        }
    }
}
