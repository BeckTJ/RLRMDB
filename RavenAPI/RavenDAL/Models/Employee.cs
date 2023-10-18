using System;
using System.Collections.Generic;

namespace RavenDAL.Models;

public partial class Employee
{
    public string EmployeeId { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public virtual ICollection<ProductRun> ProductRuns { get; set; } = new List<ProductRun>();

    public virtual ICollection<RawMaterial> RawMaterials { get; set; } = new List<RawMaterial>();

    public virtual ICollection<SampleSubmit> SampleSubmits { get; set; } = new List<SampleSubmit>();
}
