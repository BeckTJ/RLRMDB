
namespace Shared.DTO
{
    public record CreateRawMaterialDTO
    {
        public int MaterialNumber { get; set; }
        public string VendorLotNumber { get; set; } = null!;
        public int BatchNumber { get; set; }
        public string? ContainerNumber { get; set; } = null;
        public int DrumWeight { get; set; } = 180;
        public long InspectionLotNumber { get; set; }
        public int Quantity { get; set; }
    }
}