
namespace Shared.DTO
{
    public record RawMaterialDTO
    {
        public string? ProductId { get; set; }
        public int BatchNumber { get; set; }
        public string? ContainerNumber { get; set; }
        public SampleDTO SampleSubmitNumber { get; set; } 
        public int DrumWeight { get; set; }
        public long InspectionLotNumber { get; set; }
    }
}