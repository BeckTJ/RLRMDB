using AutoMapper;
using Contracts;
using Shared.DTO;

namespace Service.src
{
    internal sealed class VendorLot
    {
        private readonly IRepoManager _repo;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public VendorLot(IRepoManager repo, ILoggerManager logger, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
        }
        private VendorLotDTO CreateVendorLot(CreateRawMaterialDTO rawMaterial)
        {
            VendorLotDTO vendorLot = new()
            {
                VendorLotNumber = rawMaterial.VendorLotNumber,
                SampleSubmitNumber = null, //Create a sample id 
                Quantity = rawMaterial.Quantity,
            };
            var lot = _mapper.Map<RavenDB.Models.VendorLot>(vendorLot);
            _repo.VendorLot.Create(lot);

            return vendorLot;
        }
        public VendorLotDTO VerifyMaterialVendorLot(CreateRawMaterialDTO rawMaterial)
        {
            Sampling sampleRequired = new(_repo,_logger,_mapper);
            RawMaterialDrum rawMaterialDrum = new(_repo,_logger,_mapper);
            
            var vendorLot = _mapper.Map<VendorLotDTO>(_repo.VendorLot.GetVendorByVendorLot(rawMaterial.VendorLotNumber))
                ?? CreateVendorLot(rawMaterial);
            var sample = sampleRequired.VerifySampleRequired(rawMaterial.ParentMaterialNumber);
            
            //check required sample
            if(sample.Any(x => x.Vln == "New") && sample.Any(x => x.Vln == "Old"))
            {
                //all drums
                vendorLot.RawMaterials = rawMaterialDrum.CreateListOfDrums(rawMaterial);
            }
            else
            {
                //one drum

            }
            return vendorLot;
        }
    }
}