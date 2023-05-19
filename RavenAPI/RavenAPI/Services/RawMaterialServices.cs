using Microsoft.AspNetCore.Mvc;
using RavenAPI.DTO;
using RavenAPI.src;

namespace RavenAPI.Services;

public class RawMaterialServices
{
    internal static ActionResult<List<RawMaterialDTO>> Get(int materialNumber)
    {
        throw new NotImplementedException();
    }

    internal static ActionResult<List<RawMaterialDTO>> GetAll()
    {
        throw new NotImplementedException();
    }

    public static RawMaterialDTO GetRawMaterial(string rawMaterial)
    {
        if (RawMaterialDTO.GetRawMaterialByDrumNumber(rawMaterial) != null)
            return RawMaterialDTO.GetRawMaterialByDrumNumber(rawMaterial);
        else
            return RawMaterialDTO.SetRawMaterialFromVendorBatch(rawMaterial);
    }

    public static ProductLot NewRun(int materialNumber, string vendor) => ProductLot.StartNewRun(materialNumber, vendor);


}