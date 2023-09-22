
namespace Contracts
{
    public interface IRepoWrapper
    {
        IMaterialRepo Material { get; }
        IRawMaterialRepo RawMaterial { get; }
        void Save();
    }
}
