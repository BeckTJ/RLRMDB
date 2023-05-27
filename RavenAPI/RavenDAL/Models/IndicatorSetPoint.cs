using System;
using System.Collections.Generic;

namespace RavenDAL.Models
{
    public partial class IndicatorSetPoint
    {
        public int SystemId { get; set; }
        public string? IndicatorType { get; set; }
        public bool IsRequired { get; set; }
        public int MaterialNumber { get; set; }
        public string Nomenclature { get; set; } = null!;
        public string? Indicator { get; set; }
        public decimal? SetPoint { get; set; }
        public decimal? Variance { get; set; }

        public virtual Material MaterialNumberNavigation { get; set; } = null!;
        public virtual SystemNomenclature NomenclatureNavigation { get; set; } = null!;
    }
}
