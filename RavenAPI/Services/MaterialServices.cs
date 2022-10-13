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
         materialNumber = m.ParentMaterialNumber,
         materialName = m.MaterialNameAbreviation
     })
        .ToList();
    static public List<MaterialDTO> GetAll() => Materials;

    static public MaterialDTO Get(int id) => Materials.FirstOrDefault(m => m.materialNumber == id);

    static public void Add(Material material)
    {

    }
    static public void Delete(int id)
    {

    }
}