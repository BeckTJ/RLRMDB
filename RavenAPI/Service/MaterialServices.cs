using AutoMapper;
using Contracts;
using Service.Contracts;
using Service.Repo.Contracts;
using Shared.DTO;

namespace Service
{
    internal sealed class MaterialServices : IMaterialServices
    {
        private readonly IServiceRepoManager _repo;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public MaterialServices(IServiceRepoManager repo, ILoggerManager logger, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<IEnumerable<MaterialDTO>> GetAllMaterials()
        {
                var materials = await _repo.HighPurityMaterial.GetAllMaterial();

                return materials;
        }

        public async Task<MaterialDTO> GetMaterialByMaterialNumber(int materialNumber)
        {
            var materials = await _repo.HighPurityMaterial.GetMaterialByMaterialNumber(materialNumber);

            return materials;
        }
    }
}
