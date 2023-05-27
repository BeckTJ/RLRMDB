using RavenDAL.Models;
using RavenDAL.Data;

namespace RavenBAL.DTO;

public class ProductDTO : MaterialDTO
{
    static RavenDBContext ctx = new RavenDBContext();

    public string? ProductLotNumber { get; set; }
    public int? ProductBatchNumber { get; set; }
    public long? ProcessOrder { get; set; }
    public string? ReceiverName { get; set; }
    public SampleDTO? SampleSubmit { get; set; }
    public List<RunDTO>? ProductRun { get; set; }

    public static void SetProductLot(ProductDTO productDTO)
    {
        Production product = new Production();

        product.ProductLotNumber = productDTO.ProductLotNumber;
        product.MaterialNumber = productDTO.MaterialNumber;
        product.ProcessOrder = productDTO.ProcessOrder;
        product.ProductBatchNumber = productDTO.ProductBatchNumber;
        product.ReceiverName = productDTO.ReceiverName;

        ctx.Productions.Add(product);
        ctx.SaveChanges();
    }
    public static List<ProductDTO> GetProductLot()
    {

        return ctx.Productions
        .OrderByDescending(x => x.ProductLotNumber)
        .Select(x => new ProductDTO
        {
            ProductLotNumber = x.ProductLotNumber,
            MaterialNumber = x.MaterialNumber,
            ProductBatchNumber = x.ProductBatchNumber,
            ProcessOrder = x.ProcessOrder,
            ReceiverName = x.ReceiverName,
            SampleSubmit = SampleDTO.GetSample(x.SampleSubmitNumber),
        }).ToList();

    }
    public static List<ProductDTO> GetProductLot(int materialNumber)
    {

        return ctx.Productions
            .Where(x => x.MaterialNumber == materialNumber)
            .OrderByDescending(x => x.ProductLotNumber)
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
    }
    public static ProductDTO GetProductLot(string lotNumber)
    {

        return ctx.Productions
        .Select(x => new ProductDTO
        {
            ProductLotNumber = x.ProductLotNumber,
            MaterialNumber = x.MaterialNumber,
            ProductBatchNumber = x.ProductBatchNumber,
            ProcessOrder = x.ProcessOrder,
            ReceiverName = x.ReceiverName,
            SampleSubmit = SampleDTO.GetSample(x.SampleSubmitNumber),
            ProductRun = RunDTO.GetRuns(x.ProductLotNumber),
        })
        .Where(x => x.ProductLotNumber == lotNumber)
        .First();
    }
    public static ProductDTO GetCurrentProductLot(int materialNumber)
    {

        return ctx.Productions
        .Select(x => new ProductDTO
        {
            ProductLotNumber = x.ProductLotNumber,
            MaterialNumber = x.MaterialNumber,
            ProductBatchNumber = x.ProductBatchNumber,
            ProcessOrder = x.ProcessOrder,
            ReceiverName = x.ReceiverName,
            SampleSubmit = SampleDTO.GetSample(x.SampleSubmitNumber),
            ProductRun = RunDTO.GetRuns(x.ProductLotNumber)
        })
        .Where(x => x.MaterialNumber == materialNumber)
        .OrderByDescending(x => x.ProductLotNumber)
        .FirstOrDefault();
    }
    public static List<string> GetReceivers(int materialNumber)
    {
        return ctx.SystemReceivers
            .Where(x => x.MaterialNumber == materialNumber)
            .Select(x => x.ReceiverName).ToList();
    }


}
