using Shared.DTO;

namespace Service.Contracts
{
    public interface IMaterialServices
    {
        Task<IEnumerable<MaterialDTO>> GetAllMaterials();
        Task<MaterialDTO> GetMaterialByMaterialNumber(int materialNumber);
    }
}
