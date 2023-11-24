using AutoMapper;
using Contracts;
using RavenDB.Exceptions;
using Service.Contracts;
using Shared.DTO;

namespace Service
{
    internal sealed class MaterialServices : IMaterialServices
    {
        private readonly IRepoManager _repo;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public MaterialServices(IRepoManager repo, ILoggerManager logger, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
        }
        public IEnumerable<MaterialDTO> GetAllMaterials()
        {
                var materials = _repo.Material.GetAllMaterial();
                var materialDTO = _mapper.Map<IEnumerable<MaterialDTO>>(materials);

                return materialDTO;
        }

        public MaterialDTO GetMaterialByMaterialNumber(int materialNumber)
        {
            var materials = _repo.Material.GetMaterialByMaterialNumber(materialNumber);
            if (materials == null)
                throw new MaterialNotFoundException(materialNumber);
            var materialDTO = _mapper.Map<MaterialDTO>(materials);

            return materialDTO;
        }
    }
}
