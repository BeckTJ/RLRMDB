
using RavenDAL.Models;

namespace RavenBAL.Interface
{
    public interface IProductLotNumber
    {
        string CreateProductLotNumber(MaterialVendor material);
        string UpdateProductLotNumber(string lotNumber);
    }
}
