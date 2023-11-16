using RavenDAL.Models;
using RavenDAL.Data;

namespace RavenDAL.DTO;

public class MaterialVendorDTO
{
    public int MaterialNumber { get; set; }
    public string? VendorName { get; set; }
    public IEnumerable<VendorLot> VendorLots { get; set; }
}
