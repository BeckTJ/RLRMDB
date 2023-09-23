using RavenDAL.Models;
using RavenDAL.Data;

namespace RavenDAL.DTO;
public class MaterialDTO
{
    public int MaterialNumber { get; set; } //Material
    public string? MaterialAbrev { get; set; } //Material
    public IEnumerable<int> VendorMaterialNumber { get; set; }
}