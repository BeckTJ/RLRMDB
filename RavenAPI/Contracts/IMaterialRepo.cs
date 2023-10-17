using RavenDAL.Models;


namespace Contracts
{
    public interface IMaterialRepo:IRepoBase<Material>
    {
        IEnumerable<Material> GetAllMaterial();
        Material GetMaterialByMaterialNumber(int materialNumber);
        Material GetParentMaterialNumberFromChild(int materialNumber);

    }
}
