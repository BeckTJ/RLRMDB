
namespace Shared.DTO
{
    public record VendorLotDTO
    {
        public int MaterialNumber { get; set; }
        public string? VendorLotNumber { get; set; }
        public int Quantity { get; set; }
        public string? SampleSubmitNumber { get; set; }
        public IEnumerable<RawMaterialDrumDTO>? RawMaterials { get; set; }
    }
}