
namespace Service.Contracts
{
    public interface IServiceManager
    {
        IRawMaterialServices RawMaterialService { get; }
        IMaterialServices MaterialServices { get; }
        ISamplingServices SamplingServices { get; }
    }
}
