
namespace Shared.DTO;

public record MaterialVendorDTO
{
    public int MaterialNumber { get; set; }
    public string? VendorName { get; set; }
    public IEnumerable<VendorLotDTO> VendorLots { get; set; }
}
