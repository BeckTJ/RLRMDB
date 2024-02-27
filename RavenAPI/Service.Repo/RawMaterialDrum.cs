using AutoMapper;
using Contracts;
using RavenDB.Models;
using Service.Repo.Contracts;
using Shared.DTO;

namespace Service.Repo
{
    internal sealed class RawMaterialDrum : IRawMaterialDrum
    {
        private readonly IRepoManager _repo;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public RawMaterialDrum(IRepoManager repo,ILoggerManager logger, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
        }
        //before sample submit
        public async Task<RawMaterialDrumDTO> CreateRawMaterialDrum(CreateRawMaterialDTO rawMaterial)
        {
            var vendor = await _repo.MaterialVendor.GetMaterialVendor(rawMaterial.MaterialNumber);
            //Verify what requirnments are for type of material. (i.e. batch managed, inspection lot)
            VerifyInputRequirnments(rawMaterial, vendor);
            RawMaterialDrumDTO drum = new RawMaterialDrumDTO
            {
                MaterialNumber = rawMaterial.MaterialNumber,
                DrumLotNumber = vendor.CurrentSequenceId + vendor.MaterialCode,
                BatchNumber = rawMaterial.BatchNumber,
                InspectionLotNumber = rawMaterial.InspectionLotNumber,
                ContainerNumber = rawMaterial.ContainerNumber,
                DrumWeight = rawMaterial.DrumWeight,
                VendorLotNumber = rawMaterial.VendorLotNumber,
            };
            _repo.RawMaterial.Create(_mapper.Map<RawMaterial>(drum));
            return drum;
        }
        private void VerifyInputRequirnments(CreateRawMaterialDTO rawMaterial, MaterialVendor vendor)
        {
            /*
            *if required: 
            *  inspection lot number
            *  batch managed
            *  container number
            */
            if(vendor.BatchManaged)
            {
                //rawMaterial.BatchNumber != null;
            }
            if (vendor.VendorName == "Reclaim")
            {
                //required -> inspection lot number
            }
            if (vendor.ContainerRequired)
            {

            }
        }

        //after sample submit
        public void /*async Task<RawMaterialDrumDTO>*/ SubmitRawMaterialSample(RawMaterialDrumDTO drum)
        {
            var rawMaterial = _repo.RawMaterial.GetRawMaterialByProductId(drum.DrumLotNumber);

            _repo.RawMaterial.Update(_mapper.Map<RawMaterial>(drum));
        }

    }
}
