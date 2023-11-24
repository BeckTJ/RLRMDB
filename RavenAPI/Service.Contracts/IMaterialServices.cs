using Shared.DTO;

namespace Service.Contracts
{
    public interface IMaterialServices
    {
        IEnumerable<MaterialDTO> GetAllMaterials();
        MaterialDTO GetMaterialByMaterialNumber(int materialNumber);
    }
}
