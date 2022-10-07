using System;
using System.Collections.Generic;

namespace RavenAPI.Models
{
    public partial class Employee
    {
        public Employee()
        {
            ProductRuns = new HashSet<ProductRun>();
            RawMaterials = new HashSet<RawMaterial>();
            SampleSubmits = new HashSet<SampleSubmit>();
        }

        public string EmployeeId { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public virtual ICollection<ProductRun> ProductRuns { get; set; }
        public virtual ICollection<RawMaterial> RawMaterials { get; set; }
        public virtual ICollection<SampleSubmit> SampleSubmits { get; set; }
    }
}
