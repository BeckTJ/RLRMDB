using RavenAPI.Models;
using RavenAPI.Data;
using RavenAPI.DTO;

namespace RavenAPI.Services;
public class MaterialServices
{
    static RavenDBContext context = new RavenDBContext();

    static List<MaterialDTO> Materials { get; } = context.Materials
     .Select(m => new MaterialDTO
     {
         materialNumber = m.MaterialNumber,
         materialName = m.MaterialNameAbreviation,
         material = m.MaterialName,
     })
        .ToList();
    public static List<MaterialDTO> GetAll() => Materials;

    public static MaterialDTO Get(int id) => Materials.FirstOrDefault(m => m.materialNumber == id);

    static void Add(Material material)
    {

    }
    static void Delete(int id)
    {

    }
}