
namespace Shared.DTO
{
    public record VendorLotDTO
    {
        public string? VendorLotNumber { get; set; }
        public int Quantity { get; set; }
        public string? SampleSubmitNumber { get; set; }
        public IEnumerable<RawMaterialDTO>? RawMaterials { get; set; }
    }
}
