using AutoMapper;
using Contracts;
using RavenDB.Exceptions;
using RavenDB.Models;
using Service.Contracts;
using Shared.DTO;

namespace Service
{
    internal sealed class MaterialVendorServices : IMaterialVendorServices
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
        public MaterialVendorDTO GetMaterialVendor(int materialNumber)
        {
            var materialVendor = _repo.MaterialVendor.GetMaterialVendor(materialNumber);
            
            if(materialVendor is null) throw new MaterialNotFoundException(materialNumber);

            return _mapper.Map<MaterialVendorDTO>(materialVendor);
        }
        public IEnumerable<MaterialVendorDTO> GetApprovedRawMaterial(int parentMaterialNumber)
        {
            throw new NotImplementedException();
        }
        public void InputRawMaterial(CreateRawMaterialDTO material)
        {
            throw new NotImplementedException();
        }
    }
}
