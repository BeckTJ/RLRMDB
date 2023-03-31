using RavenAPI.Models;
using RavenAPI.DTO;

namespace RavenAPI.src;

public class LotNumber
{
    static RavenDBContext ctx = new RavenDBContext();
    public static ProductDTO GetProductLotNumber(int materialNumber)
    {
        return ctx.Productions
        .Select(x => new ProductDTO
        {
            ProductLotNumber = x.ProductLotNumber,
            MaterialNumber = x.MaterialNumber,
        })
        .Where(x => x.MaterialNumber == materialNumber)
        .OrderByDescending(x => x.ProductLotNumber)
        .FirstOrDefault();
    }

    public static string GetFirstLotNumber(int materialNumber)
    {
        string lotNumber;
        MaterialDTO material = new MaterialDTO();

        int id = (int)(from MaterialId in ctx.MaterialIds
                       where MaterialId.MaterialNumber == materialNumber
                       select MaterialId.SequenceId).First();

        material.MaterialCode = ctx.MaterialIds
                    .Where(x => x.MaterialNumber == materialNumber)
                    .Select(x => x.MaterialCode).First();

        lotNumber = id + material.MaterialCode;

        return lotNumber;
    }

    public static string GetNextProductLotNumber(int materialNumber)
    {
        ProductDTO product = GetProductLotNumber(materialNumber);
        string lotNumber;
        string code = MaterialDTO.getMaterialCode(materialNumber);
        int id;

        if (product.ProductLotNumber == null)
        {
            lotNumber = GetFirstLotNumber(materialNumber);
            return lotNumber;
        }
        else
        {
            if (product.ProductLotNumber.Length == 10 || product.ProductLotNumber.Length == 6)
            {
                id = int.Parse(product.ProductLotNumber.Substring(0, 4)) + 1;
            }
            else
            {
                id = int.Parse(product.ProductLotNumber.Substring(0, 3)) + 1;
            }
            lotNumber = id + code;
            return lotNumber;
        }
    }

    private static int resetProductId(int productId, int materialNumber)
    {

        if (GetNumberOfRecords(materialNumber) - productId == 0)
        {
            return (int)ctx.MaterialIds
                .Where(x => x.MaterialNumber == materialNumber)
                .Select(x => x.SequenceId).FirstOrDefault();
        }
        return productId;
    }
    private static int GetNumberOfRecords(int materialNumber)
    {
        return (int)ctx.MaterialIds
            .Where(x => x.MaterialNumber == materialNumber)
            .Select(x => x.TotalRecords).FirstOrDefault();
    }

}