using Microsoft.AspNetCore.Mvc;
using RavenAPI.DTO;

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

    public static object RawMaterialSelection(int materialNumber, string vendor)
    {

        if (SampleDTO.SampleRequired(MaterialDTO.GetParentMaterialNumber(materialNumber)))
        {
            return RawMaterialDTO.GetRawMaterial(materialNumber, vendor);
        }
        else
        {
            return VendorDTO.GetRawMaterial(materialNumber, vendor);
        }
    }
    public static RawMaterialDTO GetRawMaterial(string rawMaterial)
    {
        return RawMaterialDTO.SetRawMaterialFromVendorBatch(rawMaterial);
    }
}