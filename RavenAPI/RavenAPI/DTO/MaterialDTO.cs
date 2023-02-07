using RavenAPI.Models;
namespace RavenAPI.DTO;
public class MaterialDTO
{
    public string? materialName { get; set; }
    public int materialNumber { get; set; }
    public string? material { get; set; }
    public string? productCode { get; set; }

    public static List<int> getMaterialNumberFromParent(int materialNumber)
    {
        RavenDBContext ctx = new RavenDBContext();

        return ctx.MaterialNumbers
        .Where(x => x.ParentMaterialNumber == materialNumber)
        .Select(x => x.MaterialNumber1).ToList();
    }
}