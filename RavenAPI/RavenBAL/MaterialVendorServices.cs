using AutoMapper;
using Contracts;
using RavenDB.Models;
using Shared.DTO;

namespace Service
{
    public class MaterialVendorServices
    {
        private readonly IRepoManager _repo;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public MaterialVendorServices(IRepoManager repo, ILoggerManager logger, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
        }
        public MaterialVendorDTO GetMaterialVendor(int parentMaterialNumber,string vendorName)
        {
            var vendor = _repo.MaterialVendor.GetMaterialVendorFromParent(parentMaterialNumber)
                .Where(x => x.VendorName.Equals(vendorName));
            return _mapper.Map<MaterialVendorDTO>(vendor);
        }
        public void CreateVendorLot(CreateRawMaterialDTO RawMaterial)
        {
            _repo.Vendor.Create(_mapper.Map<VendorLot>(new CreateVendorLotDTO{
                MaterialNumber = RawMaterial.MaterialNumber,
                VendorLotNumber = RawMaterial.VendorLotNumber,
                SampleId = RawMaterial.SampleSubmitNumber,
                Quantity = RawMaterial.Quantity,
            }));
        }
    }
}
