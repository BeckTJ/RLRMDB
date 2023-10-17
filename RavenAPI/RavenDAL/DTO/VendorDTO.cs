using RavenDAL.Models;
using RavenDAL.Data;

namespace RavenDAL.DTO;

public class VendorDTO
{
    public int MaterialNumber { get; set; }
    public string? VendorName { get; set; }
    public string? VendorLotNumber { get; set; }
    public int Quantity { get; set; }
    public IEnumerable<RawMaterialDTO>? RawMaterials { get; set; } = new List<RawMaterialDTO>();
} 