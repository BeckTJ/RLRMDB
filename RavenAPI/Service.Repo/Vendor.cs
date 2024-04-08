using AutoMapper;
using Contracts;
using RavenDB.Models;
using Service.Repo.Contracts;
using Shared.DTO;

namespace Service.Repo
{
    internal sealed class Vendor : IVendor
    {
        private readonly IRepoManager _repo;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public Vendor(IRepoManager repo, ILoggerManager logger, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<MaterialVendorDTO> GetApprovedRawMaterial(int parentMaterialNumber)
        {
            throw new NotImplementedException();
        }
        public async Task<MaterialVendorDTO> GetMaterialVendor(int materialNumber)
        {
            var vendor = await _repo.MaterialVendor.GetMaterialVendorWithVendorLots(materialNumber);
            var materialVendor = _mapper.Map<MaterialVendorDTO>(vendor);

            return materialVendor;    
        }
        public void InputVendorLot(CreateRawMaterialDTO material)
        {
            VendorLotDTO vendorLot = new()
            {
                MaterialNumber = material.MaterialNumber,
                VendorLotNumber = material.VendorLotNumber,
                Quantity = material.Quantity,
            };

            var lot = _repo.VendorLot.GetVendorByVendorLot(material.VendorLotNumber);

            if(lot == null)
                AddVendorLot(vendorLot);
            else
                UpdateVendorLot(vendorLot);
        }
        public void AddVendorLot(VendorLotDTO vendorLot)
        {
            _repo.VendorLot.SubmitVendorLot(_mapper.Map<VendorLot>(vendorLot));
        }
        public void UpdateVendorLot(VendorLotDTO vendorLot)
        {
            _repo.VendorLot.UpdateVendorLot(_mapper.Map<VendorLot>(vendorLot));
        }
    }
}