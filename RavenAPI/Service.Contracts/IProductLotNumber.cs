
using RavenDB.Models;

namespace Service.Contracts
{
    public interface IProductLotNumber
    {
        string CreateProductLotNumber(MaterialVendor material);
        string UpdateProductLotNumber(string lotNumber);
    }
}
