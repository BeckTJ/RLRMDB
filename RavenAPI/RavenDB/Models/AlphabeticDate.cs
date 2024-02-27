using System;
using System.Collections.Generic;

namespace RavenDB.Models;

public partial class AlphabeticDate
{
    public int MonthNumber { get; set; }

    public string AlphabeticCode { get; set; } = null!;
}
