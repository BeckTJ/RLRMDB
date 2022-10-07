using System;
using System.Collections.Generic;

namespace RavenAPI.Models
{
    public partial class MaterialNumber
    {
        public MaterialNumber()
        {
            PreStartChecks = new HashSet<PreStartCheck>();
            Productions = new HashSet<Production>();
            RawMaterials = new HashSet<RawMaterial>();
            SampleRequireds = new HashSet<SampleRequired>();
            VendorBatches = new HashSet<VendorBatch>();
        }

        public int MaterialNumber1 { get; set; }
        public int ParentMaterialNumber { get; set; }
        public bool BatchManaged { get; set; }
        public bool RequiresProcessOrder { get; set; }
        public string? UnitOfIssue { get; set; }
        public bool IsRawMaterial { get; set; }

        public virtual Material ParentMaterialNumberNavigation { get; set; } = null!;
        public virtual ICollection<PreStartCheck> PreStartChecks { get; set; }
        public virtual ICollection<Production> Productions { get; set; }
        public virtual ICollection<RawMaterial> RawMaterials { get; set; }
        public virtual ICollection<SampleRequired> SampleRequireds { get; set; }
        public virtual ICollection<VendorBatch> VendorBatches { get; set; }
    }
}
