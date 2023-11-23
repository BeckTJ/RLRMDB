using RavenDB.Models;

namespace RavenDB.DTO;

public class MaterialVendorDTO
{
    public int MaterialNumber { get; set; }
    public string? VendorName { get; set; }
    public IEnumerable<VendorLot> VendorLots { get; set; }
}
