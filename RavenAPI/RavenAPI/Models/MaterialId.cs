namespace RavenAPI.Models
{
    public partial class MaterialId
    {
        public int MaterialNumber { get; set; }
        public string VendorName { get; set; } = null!;
        public string? MaterialCode { get; set; }
        public int? SequenceId { get; set; }
        public int? TotalRecords { get; set; }
    }
}
