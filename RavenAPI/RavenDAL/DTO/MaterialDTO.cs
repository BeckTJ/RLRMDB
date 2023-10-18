using RavenDAL.Models;
using RavenDAL.Data;

namespace RavenDAL.DTO;
public class MaterialDTO
{
    public int MaterialNumber { get; set; } 
    public string? MaterialAbrev { get; set; } 
    public IEnumerable<MaterialVendorDTO> MaterialVendors { get; set; }
}