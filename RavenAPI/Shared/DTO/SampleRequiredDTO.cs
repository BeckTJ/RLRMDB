
namespace Shared.DTO
{
    public record SampleRequiredDTO 
    {
        public int MaterialNumber { get; set; }
        public string? MaterialType { get; set; }
        public string? Vln { get; set; }
        public bool Assay { get; set; }
        public bool Water { get; set; }
        public bool Metals { get; set; }
        public bool Chloride { get; set; }
        public bool Boron { get; set; }
        public bool Phosphorus { get; set; }

    }
}
