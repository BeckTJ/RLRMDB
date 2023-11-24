
namespace Shared.DTO
{
    public record SampleRequiredDTO 
    {
        public int MaterialNumber { get; set; }
        public string MaterialType { get; set; }
        public List<string> VLN { get; set; }
    }
}
