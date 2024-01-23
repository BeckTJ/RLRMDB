using RavenDB.Models;


namespace Contracts
{
    public interface IMaterialRepo:IRepoBase<Material>
    {
        Task<IEnumerable<Material>> GetAllMaterial();
        Task<Material> GetMaterialByMaterialNumber(int materialNumber);
        Task<Material> GetParentMaterialNumberForChild(int materialNumber);

    }
}
