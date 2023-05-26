using RavenAPI.DTO;
using RavenAPI.Models;

namespace RavenAPI.src;

public class LotNumber
{

    public static string SetMaterialSequenceId(int materialNumber)
    {

        MaterialDTO materialId = new MaterialDTO(materialNumber);
        return materialId.SequenceId + materialId.MaterialCode;
    }
    public static string GetFirstLotNumber(int materialNumber)
    {

        return MaterialDTO.GetSequenceId(materialNumber) + MaterialDTO.getMaterialCode(materialNumber);
    }
    public static string GetLastMaterialLotNumber(int materialNumber)
    {
         RavenDBContext ctx = new RavenDBContext();

        return ctx.Productions
            .Where(x => x.MaterialNumber == materialNumber)
            .OrderByDescending(x => x.ProductLotNumber)
            .Select(x => x.ProductLotNumber)
            .FirstOrDefault();
    }

    public static string GetNextProductLotNumber(int materialNumber)
    {


        string code = MaterialDTO.getMaterialCode(materialNumber);
        int id;

        string product = GetLastMaterialLotNumber(materialNumber);

        if (product != null && product != "")
        {
            if (product.Length == 10 || product.Length == 6)
            {
                id = int.Parse(product.Substring(0, 4)) + 1;
            }
            else
            {
                id = int.Parse(product.Substring(0, 3)) + 1;
            }
            return id + code;
        }
        else
        {
            return GetFirstLotNumber(materialNumber);
        }
    }
}