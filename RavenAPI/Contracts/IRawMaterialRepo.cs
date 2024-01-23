using RavenDB.Models;

namespace Contracts
{
    public interface IRawMaterialRepo : IRepoBase<RawMaterial>
    {
        Task<IEnumerable<RawMaterial>> GetAllRawMaterial();
        Task<IEnumerable<RawMaterial>> GetRawMaterialByMaterialNumber(int materialNumber);
        void CreateRawMaterial(RawMaterial rawMaterial);
        Task<IEnumerable<RawMaterial>> GetRawMaterialWithSample(int materialNumber);
        Task<RawMaterial> GetRawMaterialByProductId(string productId);
    }
} 