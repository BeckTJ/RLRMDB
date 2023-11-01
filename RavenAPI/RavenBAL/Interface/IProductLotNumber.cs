
namespace RavenBAL.Interface
{
    public interface IProductLotNumber
    {
        string CreateProductLotNumber(int materialNumber);
        string UpdateProductLotNumber(string lotNumber);
    }
}
