namespace RavenAPI.Models
{
    public partial class ProductRun
    {
        public ProductRun()
        {
            PreStartChecks = new HashSet<PreStartCheck>();
            ProductLevels = new HashSet<ProductLevel>();
        }

        public int RunId { get; set; }
        public int? RunNumber { get; set; }
        public string? DrumLotNumber { get; set; }
        public int? RawMaterialUsed { get; set; }
        public DateTime? RunStartDate { get; set; }
        public string? ProductLotNumber { get; set; }
        public string? EmployeeId { get; set; }

        public virtual RawMaterial? DrumLotNumberNavigation { get; set; }
        public virtual Employee? Employee { get; set; }
        public virtual Production? ProductLotNumberNavigation { get; set; }
        public virtual ICollection<PreStartCheck> PreStartChecks { get; set; }
        public virtual ICollection<ProductLevel> ProductLevels { get; set; }
    }
}
