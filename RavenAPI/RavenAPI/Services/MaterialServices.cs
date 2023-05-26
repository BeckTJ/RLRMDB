using RavenAPI.DTO;
using RavenAPI.Models;

namespace RavenAPI.Services;
public class MaterialServices
{
    static RavenDBContext context = new RavenDBContext();

    static List<MaterialDTO> Materials { get; } = context.Materials
     .Select(m => new MaterialDTO
     {
         MaterialNumber = m.MaterialNumber,
         MaterialAbrev = m.MaterialNameAbreviation,
     }).ToList();

    static List<VendorDTO> Vendors { get; } = context.MaterialNumbers
    .Join(context.MaterialIds, mn => mn.MaterialNumber1, mi => mi.MaterialNumber, (mn, mi) => new VendorDTO
    {
        ParentMaterialNumber = mn.ParentMaterialNumber,
        MaterialNumber = mi.MaterialNumber,
        VendorName = mi.VendorName,
    }).ToList();

    static List<RawMaterialDTO> RawMaterial { get; }

    public static List<MaterialDTO> GetAll() => Materials;

    public static MaterialDTO Get(int id)
        => Materials.FirstOrDefault(m => m.MaterialNumber == id);
    public static List<VendorDTO> GetVendors(int parentMaterialNumber)
        => Vendors.Where(m => m.ParentMaterialNumber == parentMaterialNumber).ToList();

    static void Add(Material material)
    {

    }
    static void Delete(int id)
    {

    }
}