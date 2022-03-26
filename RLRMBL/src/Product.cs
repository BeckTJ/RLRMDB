using System;
using RLRMBL.Context;
using RLRMBL.Models;

namespace RLRMBL
{
    public class Product
    {
        public int number { get; set; }
        public string name { get; set; } = "";
        public string nameAbreviation { get; set; } = "";
        public string? productCode { get; private set; }
        public string? rawMaterialCode { get; private set; }
        public string permitNumber { get; set; } = "";
        public string unitOfIssue { get; set; } = "";
        public bool carbonDrumRequired { get; set; } = false;
        public int? carbonDrumDaysAllowed { get; private set; } = 0;
        public int? carbonDrumWeightAllowed { get; private set; } = 0;
        public bool batchManaged { get; set; } = false;
        public bool isRawMaterial { get; set; } = false;
        public bool processOrderRequired { get; set; } = false;
        public int sequenceIdEnd { get; set; } = 0;
        public int sequenceIdStart { get; set; } = 0;
        public string vendor { get; set; } = "";
        public int nameId { get; private set; }
        public int materialId { get; private set; }

        RLRMDBContext context = new RLRMDBContext();

        public Product()
        {
            number = 0;
            name = "";
            nameAbreviation = "";
            productCode = "";
            rawMaterialCode = "";
            permitNumber = "";
            unitOfIssue = "";
            carbonDrumRequired = false;
            carbonDrumDaysAllowed = 0;
            carbonDrumWeightAllowed = 0;
            batchManaged = false;
            isRawMaterial = false;
            processOrderRequired = false;
            sequenceIdStart = 0;
            sequenceIdEnd = 0;
            vendor = "";
        }
        public Product getProductByMaterialNumber(int input, string name) // rework this
        {
            Product product = new Product();
            MaterialNumber num = new MaterialNumber();
            Material mat = new Material();
            Vendor ven = new Vendor();

            var r = num.getMaterialNumber(input);
            product.number = r.MaterialNumber1;
            product.batchManaged = r.BatchManaged;
            product.processOrderRequired = r.RequiresProcessOrder;
            product.unitOfIssue = r.UnitOfIssue;
            product.isRawMaterial = r.IsRawMaterial;

            var m = mat.getMaterial(r.MaterialNameId);
            product.name = m.MaterialName;
            product.nameAbreviation = m.MaterialNameAbreviation;
            product.productCode = m.ProductCode;
            product.rawMaterialCode = m.RawMaterialCode;
            product.permitNumber = m.PermitNumber;
            product.carbonDrumRequired = m.CarbonDrumRequired;
            product.carbonDrumDaysAllowed = m.CarbonDrumDaysAllowed;
            product.carbonDrumWeightAllowed = m.CarbonDrumWeightAllowed;

            var v = ven.getVendor(name);
            product.vendor = v.VendorName;

            return product;
        }
        public void updateMaterial(int input)
        {
            MaterialNumber n = new MaterialNumber();
            var mn = n.getMaterialNumber(input);
            Material m = new Material();
            m.getMaterial(mn.MaterialNameId);
            MaterialId id = new MaterialId();
            var mid = id.getMaterialId(input);

            m.MaterialNumbers.Append(mn);
            n.MaterialIds.Append(mid);
            context.Materials.Append(m);

        }
        public Product MaterialLookupByMaterialNumber(int input)
        {
            return (from MaterialNumber in context.MaterialNumbers
                    join Material in context.Materials on MaterialNumber.MaterialNameId equals Material.MaterialNameId
                    join MaterialId in context.MaterialIds on MaterialNumber.MaterialNumber1 equals MaterialId.MaterialNumber
                    where MaterialNumber.MaterialNumber1 == input
                    select new Product
                    {
                        nameId = Material.MaterialNameId,
                        number = MaterialNumber.MaterialNumber1,
                        materialId = MaterialId.MaterialId1
                    }).First();
        }
        public Product MaterialLookupByNameAbreviation(string input, string name)
        {
            return (from Material in context.Materials
                    join MaterialNumber in context.MaterialNumbers on Material.MaterialNameId equals MaterialNumber.MaterialNameId
                    join MaterialId in context.MaterialIds on MaterialNumber.MaterialNumber1 equals MaterialId.MaterialNumber
                    join Vendor in context.Vendors on MaterialId.VendorId equals Vendor.VendorId
                    join ProductNumberSequence in context.ProductNumberSequences on MaterialId.SequenceId equals ProductNumberSequence.SequenceId
                    where Material.MaterialNameAbreviation == input && Vendor.VendorName == name
                    select new Product
                    {
                        number = MaterialNumber.MaterialNumber1,
                        nameId = Material.MaterialNameId,
                        name = Material.MaterialName,
                        nameAbreviation = Material.MaterialNameAbreviation,
                        productCode = Material.ProductCode,
                        rawMaterialCode = Material.RawMaterialCode,
                        permitNumber = Material.PermitNumber!,
                        unitOfIssue = MaterialNumber.UnitOfIssue!,
                        carbonDrumRequired = Material.CarbonDrumRequired,
                        carbonDrumDaysAllowed = Material.CarbonDrumDaysAllowed,
                        carbonDrumWeightAllowed = Material.CarbonDrumWeightAllowed,
                        batchManaged = MaterialNumber.BatchManaged,
                        isRawMaterial = MaterialNumber.IsRawMaterial,
                        processOrderRequired = MaterialNumber.RequiresProcessOrder,
                        sequenceIdStart = ProductNumberSequence.SequenceIdStart,
                        sequenceIdEnd = ProductNumberSequence.SequenceIdEnd,
                        vendor = Vendor.VendorName
                    }).First();

        }
        public void addNewMaterialToDatabase(int number, string name, string nameAbreviation, string productCode, string rawMaterialCode, string permitNumber, string unitOfIssue, bool carbonDrumRequired, int carbonDrumDaysAllowed, int carbonDrumWeightAllowed, bool batchManaged, bool isRawMaterial, bool processOrderRequired, int sequenceIdStart, string vendorName)
        {
            Material m = new Material();
            MaterialNumber n = new MaterialNumber();
            MaterialId id = new MaterialId();
            ProductNumberSequence p = new ProductNumberSequence();
            Vendor v = new Vendor();


            m.setMaterial(name, nameAbreviation, permitNumber, rawMaterialCode, productCode, carbonDrumRequired, carbonDrumDaysAllowed, carbonDrumWeightAllowed);
            n.setMaterialNumber(number, m.MaterialNameId, batchManaged, processOrderRequired, unitOfIssue, isRawMaterial);

            int vendorId = 0;
            var vendor = (v.getVendor(vendorName));
            if (vendorName == null)
                v.addVendor(vendorName);
            else
                vendorId = vendor.VendorId;
            int pnid = p.getSequenceId(sequenceIdStart).SequenceId;

            id.setMaterialId(number, vendorId, pnid, sequenceIdStart);

            m.MaterialNumbers.Add(n);
            n.MaterialIds.Add(id);
            context.Materials.Add(m);
            context.SaveChanges();
        }
        public void removeMaterialFromDatabase(int input)
        {
            MaterialNumber n = new MaterialNumber();
            var result = n.getMaterialNumber(input);
            Material m = new Material();
            m.getMaterial(result.MaterialNameId);
            MaterialId id = new MaterialId();
            var mid = id.getMaterialId(input);

            context.MaterialIds.Remove(mid);
            context.MaterialNumbers.Remove(result);
            context.Materials.Remove(m);
            context.SaveChanges();
        }
    }
}