

using Shared.DTO;

namespace Service.Contracts
{
    public interface IProductLotNumber
    {
        string CreateProductLotNumber(MaterialVendorDTO material);
        string UpdateProductLotNumber(string lotNumber);
    }
}
