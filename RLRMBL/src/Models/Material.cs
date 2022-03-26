using System;
using System.Collections.Generic;
using RLRMBL.Context;

namespace RLRMBL.Models
{
    public partial class Material
    {
        RLRMDBContext context = new RLRMDBContext();
        public Material() => MaterialNumbers = new HashSet<MaterialNumber>();

        public int MaterialNameId { get; set; } = 0;
        public string MaterialName { get; set; } = "";
        public string MaterialNameAbreviation { get; set; } = "";
        public string? PermitNumber { get; set; }
        public string? RawMaterialCode { get; set; }
        public string? ProductCode { get; set; }
        public bool CarbonDrumRequired { get; set; }
        public int? CarbonDrumDaysAllowed { get; set; }
        public int? CarbonDrumWeightAllowed { get; set; }

        public virtual ICollection<MaterialNumber> MaterialNumbers { get; set; }

        internal Material getMaterial(int input)
        {

            MaterialNameId = input;
            return (from Material in context.Materials
                    where Material.MaterialNameId == input
                    select new Material
                    {
                        MaterialName = Material.MaterialName,
                        MaterialNameAbreviation = Material.MaterialNameAbreviation,
                        PermitNumber = Material.PermitNumber,
                        RawMaterialCode = Material.RawMaterialCode,
                        ProductCode = Material.ProductCode,
                        CarbonDrumRequired = Material.CarbonDrumRequired,
                        CarbonDrumDaysAllowed = Material.CarbonDrumDaysAllowed,
                        CarbonDrumWeightAllowed = Material.CarbonDrumWeightAllowed
                    }).First();
        }
        internal Material getMaterial(string input)
        {
            return (from Material in context.Materials
                    where Material.MaterialNameAbreviation == input
                    select new Material
                    {
                        MaterialName = Material.MaterialName,
                        MaterialNameAbreviation = Material.MaterialNameAbreviation,
                        PermitNumber = Material.PermitNumber,
                        RawMaterialCode = Material.RawMaterialCode,
                        ProductCode = Material.ProductCode,
                        CarbonDrumRequired = Material.CarbonDrumRequired,
                        CarbonDrumDaysAllowed = Material.CarbonDrumDaysAllowed,
                        CarbonDrumWeightAllowed = Material.CarbonDrumWeightAllowed
                    }).First();
        }

        internal void setMaterial(string name, string nameAbreviation, string permitNumber, string rawMaterialCode, string productCode, bool carbonDrumRequired, int carbonDrumDaysAllowed, int carbonDrumWeightAllowed)
        {
            MaterialName = name;
            MaterialNameAbreviation = nameAbreviation;
            PermitNumber = permitNumber;
            RawMaterialCode = rawMaterialCode;
            ProductCode = productCode;
            CarbonDrumRequired = carbonDrumRequired;
            CarbonDrumDaysAllowed = carbonDrumDaysAllowed;
            CarbonDrumWeightAllowed = carbonDrumWeightAllowed;
        }
    }
}
