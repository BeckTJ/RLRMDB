using RavenAPI.Models;
using RavenAPI.DTO;

namespace RavenAPI.src;

public class Product
{
    static RavenDBContext context = new RavenDBContext();

    public static void setProducts(ProductDTO product)
    {


    }
    public static List<ProductDTO> getProductLot()
    {
        return context.Productions
        .Select(x => new ProductDTO
        {
            ProductLotNumber = x.ProductLotNumber,
            MaterialNumber = x.MaterialNumber,
            ProductBatchNumber = x.ProductBatchNumber,
            ProcessOrder = x.ProcessOrder,
            ReceiverId = x.ReceiverId,
            SampleSubmitNumber = x.SampleSubmitNumber,
            // StartDate = x.StartDate,
        }).ToList();
    }
    public static List<ProductDTO> getProductLot(int materialNumber)
    {
        return context.Productions
        .Select(x => new ProductDTO
        {
            ProductLotNumber = x.ProductLotNumber,
            MaterialNumber = x.MaterialNumber,
            ProductBatchNumber = x.ProductBatchNumber,
            ProcessOrder = x.ProcessOrder,
            ReceiverId = x.ReceiverId,
            SampleSubmitNumber = x.SampleSubmitNumber,
            // StartDate = x.StartDate,
        })
        .Where(x => x.MaterialNumber == materialNumber)
        .OrderBy(x => x.ProductLotNumber)
        .ToList();
    }
    public static ProductDTO getProductLot(string lotNumber)
    {
        return context.Productions
        .Select(x => new ProductDTO
        {
            ProductLotNumber = x.ProductLotNumber,
            MaterialNumber = x.MaterialNumber,
            ProductBatchNumber = x.ProductBatchNumber,
            ProcessOrder = x.ProcessOrder,
            ReceiverId = x.ReceiverId,
            SampleSubmitNumber = x.SampleSubmitNumber,
            // StartDate = x.StartDate,
        })
        .Where(x => x.ProductLotNumber == lotNumber)
        .First();
    }

    public static ProductDTO getProductLotNumber(int materialNumber)
    {
        return context.Productions
        .Select(x => new ProductDTO
        {
            ProductLotNumber = x.ProductLotNumber,
            MaterialNumber = x.MaterialNumber,
        })
        .Where(x => x.MaterialNumber == materialNumber)
        .OrderByDescending(x => x.ProductLotNumber)
        .FirstOrDefault();
    }
    public static ProductDTO getFirstLotNumber(int materialNumber)
    {
        ProductDTO lotNumber = new ProductDTO();
        MaterialDTO material = new MaterialDTO();

        int id = (from ProductNumberSequence in context.ProductNumberSequences
                  join MaterialId in context.MaterialIds on ProductNumberSequence.SequenceId equals MaterialId.SequenceId
                  where MaterialId.MaterialNumber == materialNumber
                  select ProductNumberSequence.SequenceIdStart).First();

        material.productCode = context.MaterialIds
                    .Where(x => x.MaterialNumber == materialNumber)
                    .Select(x => x.MaterialCode).First();

        lotNumber.ProductLotNumber = id + material.productCode;
        lotNumber.MaterialNumber = materialNumber;

        return lotNumber;
    }

    public static ProductDTO getNextProductLotNumber(int materialNumber)
    {
        ProductDTO lotNumber = new ProductDTO();
        string code;
        int id;
        if (getProductLotNumber(materialNumber) == null)
        {
            lotNumber = getFirstLotNumber(materialNumber);
            return lotNumber;
        }
        else
        {
            var product = getProductLotNumber(materialNumber);

            if (product.ProductLotNumber.Length == 10 || product.ProductLotNumber.Length == 6)
            {
                code = product.ProductLotNumber.Substring(5, 4);
                id = int.Parse(product.ProductLotNumber.Substring(0, 4)) + 1;
            }
            else
            {
                code = product.ProductLotNumber.Substring(3, 2);
                id = int.Parse(product.ProductLotNumber.Substring(0, 3)) + 1;
            }
            lotNumber.ProductLotNumber = id + code;
            lotNumber.MaterialNumber = materialNumber;
            return lotNumber;
        }


    }
    public static void setProductLot(ProductDTO productDTO)
    {
        Production product = new Production();

        product.ProductLotNumber = productDTO.ProductLotNumber;
        product.MaterialNumber = productDTO.MaterialNumber;
        product.ProcessOrder = productDTO.ProcessOrder;
        product.ProductBatchNumber = productDTO.ProductBatchNumber;
        Receiver receiver = context.Receivers.FirstOrDefault(x => x.ReceiverId == productDTO.ReceiverId);
        product.Receiver = receiver;

        context.Productions.Add(product);
        context.SaveChanges();
    }
}

