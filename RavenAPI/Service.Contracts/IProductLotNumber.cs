

using Shared.DTO;

namespace Service.Contracts
{
    public interface IProductLotNumber<T> where T : class
    {
        string CreateProductLotNumber(T material);
        string UpdateProductLotNumber(string lotNumber);
    }
}
