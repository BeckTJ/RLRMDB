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
        public async Task<MaterialVendorDTO> GetMaterialVendor(int materialNumber)
        {
            var materialVendor = await _repo.MaterialVendor.GetMaterialVendor(materialNumber);
            
            if(materialVendor is null) throw new MaterialNotFoundException(materialNumber);

            return _mapper.Map<MaterialVendorDTO>(materialVendor);
        }
        public async Task<IEnumerable<MaterialVendorWithRawMaterialDTO>> GetApprovedRawMaterial(int ParentMaterialNumber)
        {
            var materialVendor = await _repo.MaterialVendor.GetMaterialVendorWithRawMaterial(ParentMaterialNumber);

            foreach(var material in materialVendor)
            {
                if(material.RawMaterials is null) throw new NullReferenceException(nameof(material.RawMaterials));

            }

            if (materialVendor is null) throw new MaterialNotFoundException(ParentMaterialNumber);

            return _mapper.Map<IEnumerable<MaterialVendorWithRawMaterialDTO>>(materialVendor);
        }
        public Task InputRawMaterial(CreateRawMaterialDTO material)
        {
            throw new NotImplementedException();
        }
    }
}
