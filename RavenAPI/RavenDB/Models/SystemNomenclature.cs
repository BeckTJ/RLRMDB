using System;
using System.Collections.Generic;

namespace RavenDB.Models;

public partial class SystemNomenclature
{
    public string Nomenclature { get; set; } = null!;

    public virtual ICollection<IndicatorSetPoint> IndicatorSetPoints { get; set; } = new List<IndicatorSetPoint>();
}
