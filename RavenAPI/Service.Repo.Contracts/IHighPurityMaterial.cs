using Shared.DTO;

namespace Service.Repo.Contracts
{
    public interface IHighPurityMaterial
    {
        Task<IEnumerable<MaterialDTO>> GetAllMaterial();
        Task<MaterialDTO> GetMaterialByMaterialNumber(int materialNumber);
    }
}
