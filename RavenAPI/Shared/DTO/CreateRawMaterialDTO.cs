
namespace Shared.DTO
{
    public record CreateRawMaterialDTO
    {
        public int ParentMaterialNumber { get; set; }
        public int MaterialNumber { get; set; }
        public string? VendorName {  get; set; }
        public string? VendorLotNumber { get; set; } = null;
        public string? SampleId { get; set; }
        public int BatchNumber { get; set; } = 0;
        public string? ContainerNumber { get; set; } = null;
        public int DrumWeight { get; set; } = 180;
        public long InspectionLotNumber { get; set; } = 0;
        public int Quantity { get; set; } = 1;
    }
}