using RavenAPI.DTO;
using RavenAPI.src;

namespace RavenAPI.Services;

public class ProductServices
{
    static List<ProductDTO> Products { get; } = ProductDTO.GetProductLot();

    public static List<ProductDTO> GetAll() => Products;

    public static ProductDTO Get(string lotNumber) => ProductDTO.GetProductLot(lotNumber);

    public static ProductLot ProductSelection(int materialNumber) => ProductLot.StartNewLot(materialNumber);

    public static void InsertProductLot(ProductDTO product) => ProductDTO.SetProductLot(product);
    public static List<String> GetVendors(int materialNumber) => VendorDTO.getVendorFromParent(materialNumber);
    public static List<String> GetReceivers(int materialNumber) => ProductDTO.GetReceivers(materialNumber);
    public static ProductLot NewRun(int materialNumber, string vendor) => ProductLot.StartNewRun(materialNumber, vendor);

}