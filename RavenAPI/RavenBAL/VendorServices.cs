using AutoMapper;
using Contracts;
using RavenDB.Models;
using Shared.DTO;

namespace Service
{
    public class VendorServices
    {
        private readonly IRepoManager _repo;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public VendorServices(IRepoManager repo, ILoggerManager logger, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
        }

        public void CreateVendorLot(CreateVendorLotDTO vendorLot)
        {
            _repo.Vendor.Create(_mapper.Map<VendorLot>(vendorLot));
        }
    }
}
