using RLRMBL.Context;

namespace RLRMBL.Models
{
    public partial class MaterialNumber
    {
        RLRMDBContext context = new RLRMDBContext();
        public MaterialNumber()
        {
            MaterialIds = new HashSet<MaterialId>();
            Products = new HashSet<Production>();
            RawMaterials = new HashSet<RawMaterial>();
        }
        public int MaterialNumber1 { get; set; }
        public int MaterialNameId { get; set; }
        public bool BatchManaged { get; set; }
        public bool RequiresProcessOrder { get; set; }
        public string? UnitOfIssue { get; set; } = " ";
        public bool IsRawMaterial { get; set; }

        public virtual Material? MaterialName { get; set; }
        public virtual ICollection<MaterialId> MaterialIds { get; set; }
        public virtual ICollection<Production> Products { get; set; }
        public virtual ICollection<RawMaterial> RawMaterials { get; set; }

        internal MaterialNumber getMaterialNumber(int input)
        {
            return (from MaterialNumber in context.MaterialNumbers
                    where MaterialNumber.MaterialNumber1 == input
                    select new MaterialNumber
                    {
                        MaterialNumber1 = MaterialNumber.MaterialNumber1,
                        MaterialNameId = MaterialNumber.MaterialNameId,
                        BatchManaged = MaterialNumber.BatchManaged,
                        RequiresProcessOrder = MaterialNumber.RequiresProcessOrder,
                        UnitOfIssue = MaterialNumber.UnitOfIssue,
                        IsRawMaterial = MaterialNumber.IsRawMaterial
                    }).First();
        }

        internal void setMaterialNumber(int number, int nameId, bool batchManaged, bool processOrderRequired, string unitOfIssue, bool isRawMaterial)
        {
            MaterialNumber1 = number;
            MaterialNameId = nameId;
            BatchManaged = batchManaged;
            RequiresProcessOrder = processOrderRequired;
            IsRawMaterial = isRawMaterial;
            UnitOfIssue = unitOfIssue;
        }
    }
}
