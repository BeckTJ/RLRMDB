using RavenDB.Models;

namespace Contracts
{
    public interface IRawMaterialRepo : IRepoBase<RawMaterial>
    {
        IEnumerable<RawMaterial> GetAllRawMaterial();
        IEnumerable<RawMaterial> GetRawMaterialByMaterialNumber(int materialNumber);
        void CreateRawMaterial(RawMaterial rawMaterial);
        IEnumerable<RawMaterial> GetRawMaterialWithSample(int materialNumber);

    }
} 