
namespace RavenBAL.Interface
{
    public interface IProductLotNumber<T>
    {
        T CreateProductLotNumber(T product);
        T UpdateProductLotNumber(T product);

    }
}
