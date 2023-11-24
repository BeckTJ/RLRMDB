namespace Shared.DTO;
public record MaterialDTO
{
    public int MaterialNumber { get; init; } 
    public string? MaterialAbrev { get; init; } 
    public IEnumerable<MaterialVendorDTO>? MaterialVendors { get; init; }
}