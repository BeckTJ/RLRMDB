using System;
using System.Collections.Generic;

namespace RavenDAL.Models;

public partial class ProductLevel
{
    public int LevelId { get; set; }

    public string? ProductLotNumber { get; set; }

    public int? RunNumber { get; set; }

    public string? SystemStatus { get; set; }

    public bool? VisualVerification { get; set; }

    public int? ReboilerLevel { get; set; }

    public int? PrefractionLevel { get; set; }

    public int? ReceiverLevel { get; set; }

    public TimeSpan? ReadTime { get; set; }

    public virtual Production? ProductLotNumberNavigation { get; set; }

    public virtual ProductRun? RunNumberNavigation { get; set; }
}
