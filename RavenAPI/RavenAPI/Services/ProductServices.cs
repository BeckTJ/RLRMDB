using RavenAPI.DTO;
using RavenAPI.src;

namespace RavenAPI.Services;

public class ProductServices
{
    static List<ProductDTO> Products { get; } = Product.getProductLot();

    public static List<ProductDTO> GetAll() => Products;

    public static ProductDTO Get(string lotNumber) => Product.getProductLot(lotNumber);

    public static ProductDTO getNextProductLotNumber(int materialNumber) => Product.getNextProductLotNumber(materialNumber);

    public static void InsertProductLot(ProductDTO product) => Product.setProductLot(product);
    public static List<String> GetVendors(int materialNumber) => RawMaterialSRC.getVendor(materialNumber);
}