using RavenDAL.Models;

namespace Contracts
{
    public interface IRawMaterialRepo : IRepoBase<RawMaterial>
    {
        IEnumerable<RawMaterial> GetAllRawMaterial();
        RawMaterial GetRawMaterialByMaterialNumber(int materialNumber);
        void CreateRawMaterial(RawMaterial rawMaterial);
    }
} 