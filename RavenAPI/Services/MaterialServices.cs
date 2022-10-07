using RavenAPI.Models;
using RavenAPI.Data;
using System.Linq;

namespace RavenAPI.Services;
public class MaterialServices
{
    static RavenDBContext context = new RavenDBContext();
    static List<Material> Materials { get; } = (from Material in context.Materials
                                                select Material).ToList();
    public static List<Material> GetAll() => Materials;

    public static Material Get(int id) => Materials.FirstOrDefault(m => m.ParentMaterialNumber == id);

    public static void Add(Material material)
    {

    }
    public static void Delete(int id)
    {

    }
}