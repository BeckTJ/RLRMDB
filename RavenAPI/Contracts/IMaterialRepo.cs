using RavenDB.Models;


namespace Contracts
{
    public interface IMaterialRepo:IRepoBase<Material>
    {
        IEnumerable<Material> GetAllMaterial();
        Material GetMaterialByMaterialNumber(int materialNumber);
        Material GetParentMaterialNumberForChild(int materialNumber);

    }
}
