
namespace Shared.DTO
{
    public record RawMaterialDrumDTO
    {
        public int MaterialNumber { get; set; }
        public string? DrumLotNumber { get; set; }
        public string? VendorLotNumber { get; set; }
        public int BatchNumber { get; set; }
        public string? ContainerNumber { get; set; }
        public string? SampleSubmitNumber { get; set; } 
        public int DrumWeight { get; set; }
        public long InspectionLotNumber { get; set; }
    }
}