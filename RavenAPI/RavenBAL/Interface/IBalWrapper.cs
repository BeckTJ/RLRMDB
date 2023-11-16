
namespace RavenBAL.Interface
{
    public interface IBalWrapper
    {
        IRawMaterialService RawMaterialService { get; }
        IProductLotNumber ProductLotNumber { get; }
    }
}
