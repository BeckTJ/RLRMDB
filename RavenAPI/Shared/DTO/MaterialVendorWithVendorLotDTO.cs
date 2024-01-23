
namespace Shared.DTO;

public record MaterialVendorWithVendorLotDTO
{
    public int ParentMaterialNumber { get; set; }
    public int MaterialNumber { get; set; }
    public string? VendorName { get; set; }
    public IEnumerable<VendorLotDTO>? VendorLots { get; set; }
}
