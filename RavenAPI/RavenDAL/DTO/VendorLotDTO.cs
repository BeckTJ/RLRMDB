
namespace RavenDB.DTO
{
    public class VendorLotDTO
    {
        public int MaterialNumber { get; set; }
        public string Name { get; set; }
        public string VendorLotNumber { get; set; }
        public int Quantity { get; set; }
        public string SampleSubmitNumber { get; set; }
        public IEnumerable<RawMaterialDTO> RawMaterials { get; set; }
    }
}
