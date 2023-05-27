using RavenBAL.DTO;
using RavenBAL.src;
using RavenDAL.Models;
using RavenDAL.Data;

namespace RavenBAL.Services;

public class ProductServices
{
    static RavenDBContext ctx = new RavenDBContext();
    static List<ProductDTO> Products { get; } = ctx.Productions
            .Select(x => new ProductDTO
            {
                ProductLotNumber = x.ProductLotNumber,
                MaterialNumber = x.MaterialNumber,
                ProductBatchNumber = x.ProductBatchNumber,
                ProcessOrder = x.ProcessOrder,
                ReceiverName = x.ReceiverName,
                SampleSubmit = SampleDTO.GetSample(x.SampleSubmitNumber),
                ProductRun = RunDTO.GetRuns(x.ProductLotNumber),
            }).ToList();

    public static List<ProductDTO> GetAll() => Products;
    public static List<ProductDTO> GetProductByMaterialNumber(int materialNumber) => 
        Products.Where(p => p.MaterialNumber == materialNumber)
        .OrderByDescending(p => p.ProductLotNumber).ToList();
    public static ProductDTO GetProductByLotNumber(string lotNumber) => Products.FirstOrDefault(p => p.ProductLotNumber == lotNumber);
    public static void InsertProductLot(ProductDTO product) => ProductDTO.SetProductLot(product);
    public static List<String> GetVendors(int materialNumber) => VendorDTO.getVendorFromParent(materialNumber);
    public static List<String> GetReceivers(int materialNumber) => ProductDTO.GetReceivers(materialNumber);

    public static ProductDTO StartNewLot(int materialNumber)
    {
        RunDTO run = new RunDTO
        {
            RunNumber = 1,
        };
        ProductDTO product = new ProductDTO
        {
            MaterialNumber = materialNumber,
            ProductLotNumber = LotNumber.GetNextProductLotNumber(materialNumber)
        };

        product.ProductRun = new List<RunDTO> { run };

        return product;
    }


}