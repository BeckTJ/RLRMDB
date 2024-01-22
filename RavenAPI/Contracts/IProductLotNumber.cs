
namespace Service.Contracts
{
    public interface IProductLotNumber<T> where T : class
    {
        string CreateProductLotNumber(int materialNumber);
        string UpdateProductLotNumber(string lotNumber);
    }
}
