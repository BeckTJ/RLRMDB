using AutoMapper;
using Contracts;
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
            throw new NotImplementedException();
        }
        public void InputVendorLot(CreateRawMaterialDTO material)
        {
            throw new NotImplementedException();
        }
    }
}
