using System;
using System.Collections.Generic;

namespace RLRMBL
{
    public partial class MaterialNumber
    {
        public MaterialNumber()
        {
            MaterialIds = new HashSet<MaterialId>();
            Products = new HashSet<Product>();
            RawMaterials = new HashSet<RawMaterial>();
        }

        public int MaterialNumber1 { get; set; }
        public int? MaterialNameId { get; set; }
        public string? MaterialGrade { get; set; }
        public bool? BatchManaged { get; set; }
        public bool? RequiresProcessOrder { get; set; }
        public string? UnitOfIssue { get; set; }
        public bool? IsRawMaterial { get; set; }

        public virtual MaterialName? MaterialName { get; set; }
        public virtual ICollection<MaterialId> MaterialIds { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<RawMaterial> RawMaterials { get; set; }
    }
}
