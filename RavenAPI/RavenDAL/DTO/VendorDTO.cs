using RavenDAL.Models;
using RavenDAL.Data;

namespace RavenDAL.DTO;

public class VendorDTO
{
    public int MaterialNumber { get; set; }
    public string? VendorName { get; set; }
    public string LotNumber { get; set; }
    public IEnumerable<RawMaterialDTO> RawMaterials { get; set; }
} 