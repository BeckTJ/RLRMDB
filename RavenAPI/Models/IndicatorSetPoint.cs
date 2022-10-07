using System;
using System.Collections.Generic;

namespace RavenAPI.Models
{
    public partial class IndicatorSetPoint
    {
        public int SystemId { get; set; }
        public int? ParentMaterialNumber { get; set; }
        public string? Nomenclature { get; set; }
        public string? Indicator { get; set; }
        public decimal? SetPointLow { get; set; }
        public decimal? SetPointHigh { get; set; }
        public decimal? Variance { get; set; }

        public virtual SystemNomenclature? NomenclatureNavigation { get; set; }
        public virtual Material? ParentMaterialNumberNavigation { get; set; }
    }
}
