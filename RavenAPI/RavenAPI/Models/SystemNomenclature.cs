namespace RavenAPI.Models
{
    public partial class SystemNomenclature
    {
        public SystemNomenclature()
        {
            IndicatorSetPoints = new HashSet<IndicatorSetPoint>();
        }

        public string Nomenclature { get; set; } = null!;

        public virtual ICollection<IndicatorSetPoint> IndicatorSetPoints { get; set; }
    }
}
