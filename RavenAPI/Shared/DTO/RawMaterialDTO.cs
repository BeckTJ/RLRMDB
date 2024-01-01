
namespace Shared.DTO
{
    public record RawMaterialDTO
    {
        public string? DrumLotNumber { get; set; }
        public string? VendorLotNumber { get; set; }
        public int BatchNumber { get; set; }
        public string? ContainerNumber { get; set; }
        public string? SampleSubmitNumber { get; set; } 
        public int DrumWeight { get; set; }
        public long InspectionLotNumber { get; set; }
    }
}