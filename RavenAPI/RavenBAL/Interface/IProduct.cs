
namespace RavenBAL.Interface
{
    public interface IProduct<T>
    {
        T CreateProduct(T product);
        T SampleProduct(T product);

    }
}
