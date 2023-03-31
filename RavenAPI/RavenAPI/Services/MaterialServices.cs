using RavenAPI.Models;
using RavenAPI.DTO;

namespace RavenAPI.Services;
public class MaterialServices
{
    static RavenDBContext context = new RavenDBContext();

    static List<MaterialDTO> Materials { get; } = context.Materials
     .Select(m => new MaterialDTO
     {
         MaterialNumber = m.MaterialNumber,
         MaterialAbrev = m.MaterialNameAbreviation,
         MaterialName = m.MaterialName,
     }).ToList();
    public static List<MaterialDTO> GetAll() => Materials;

    public static MaterialDTO Get(int id) => Materials.FirstOrDefault(m => m.MaterialNumber == id);

    static void Add(Material material)
    {

    }
    static void Delete(int id)
    {

    }
}