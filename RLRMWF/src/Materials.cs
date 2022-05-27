using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLRMWF
{
    public class Materials
    {
        RLRMDBEntities context = new RLRMDBEntities();

        public int number { get; private set; }
        public int nameId { get; private set; }
        public string name { get; private set; }
        public string nameAbreviation { get; private set; }
        public string productCode { get; private set; }
        public string rawMaterialCode { get; private set; }
        public string permitNumber { get; private set; }
        public string unitOfIssue { get; private set; }
        public bool? carbonDrumRequired { get; private set; }
        public int? carbonDrumDaysAllowed { get; private set; }
        public int? carbonDrumWeightAllowed { get; private set; }
        public bool? batchManaged { get; private set; }
        public bool? isRawMaterial { get; private set; }
        public bool? processOrderRequired { get; private set; }
        public int sequenceIdStart { get; private set; }
        public int? sequenceIdEnd { get; private set; }
        public string vendor { get; private set; }
        public int? currentId { get; private set; }

        public Materials getMaterial(int input)
        {

            return (from MaterialNumber in context.MaterialNumbers
                    join Material in context.Materials on MaterialNumber.NameId equals Material.NameId
                    join MaterialId in context.MaterialIds on MaterialNumber.MaterialNumber1 equals MaterialId.MaterialNumber
                    join productNumberSequence in context.ProductNumberSequences on MaterialId.SequenceId equals productNumberSequence.SequenceId
                    where MaterialNumber.MaterialNumber1 == input
                    select new Materials
                    {
                        number = MaterialNumber.MaterialNumber1,
                        nameId = Material.NameId,
                        name = Material.MaterialName,
                        nameAbreviation = Material.MaterialNameAbreviation,
                        productCode = Material.ProductCode,
                        rawMaterialCode = Material.RawMaterialCode,
                        permitNumber = Material.PermitNumber,
                        unitOfIssue = MaterialNumber.UnitOfIssue,
                        carbonDrumRequired = Material.CarbonDrumRequired,
                        carbonDrumDaysAllowed = Material.CarbonDrumDaysAllowed,
                        carbonDrumWeightAllowed = Material.CarbonDrumWeightAllowed,
                        batchManaged = MaterialNumber.BatchManaged,
                        isRawMaterial = MaterialNumber.IsRawMaterial,
                        processOrderRequired = MaterialNumber.RequiresProcessOrder,
                        currentId = MaterialId.CurrentSequenceId,
                        sequenceIdStart = productNumberSequence.SequenceIdStart,
                        sequenceIdEnd = productNumberSequence.SequenceIdEnd,
                    }).First();
        }

        internal List<Materials> getRawMaterialFromDatabase(int nameId)
        {
            return (from MaterialNumber in context.MaterialNumbers
                    join Material in context.Materials on MaterialNumber.NameId equals Material.NameId
                    join MaterialId in context.MaterialIds on MaterialNumber.MaterialNumber1 equals MaterialId.MaterialNumber
                    join Vendor in context.Vendors on MaterialId.VendorId equals Vendor.VendorId
                    join ProductNumberSequence in context.ProductNumberSequences on MaterialId.SequenceId equals ProductNumberSequence.SequenceId
                    where MaterialNumber.NameId == nameId && MaterialNumber.IsRawMaterial == true
                    select new Materials
                    {
                        number = MaterialNumber.MaterialNumber1,
                        vendor = Vendor.VendorName,
                        sequenceIdStart = ProductNumberSequence.SequenceIdStart,
                        sequenceIdEnd = ProductNumberSequence.SequenceIdEnd,
                        currentId = MaterialId.CurrentSequenceId,
                        rawMaterialCode = Material.RawMaterialCode

                    }).ToList();
        }
        public List<int> getRawMaterialNumber(int number)
        {
            var nameId = getMaterialNameId(number);

            return (from MaterialNumber in context.MaterialNumbers
                    where MaterialNumber.NameId == nameId && MaterialNumber.IsRawMaterial == true
                    select MaterialNumber.MaterialNumber1).ToList();
        }
        public int getMaterialNameId(int MaterialNumber)
        {
            return (int)context.MaterialNumbers
                .Where(mn => mn.MaterialNumber1 == MaterialNumber)
                .Select(mn => mn.NameId).FirstOrDefault();
        }
        public List<Materials> getMaterialNameList()
        {
            return (from Material in context.Materials
                    join MaterialNumber in context.MaterialNumbers on Material.NameId equals MaterialNumber.NameId
                    where MaterialNumber.IsRawMaterial != true
                    orderby Material.MaterialName
                    select new Materials
                    {
                        number = MaterialNumber.MaterialNumber1,
                        name = Material.MaterialNameAbreviation
                    }).ToList();
        }


    }
}
