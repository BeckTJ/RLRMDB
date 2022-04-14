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
        public bool carbonDrumRequired { get; private set; }
        public int? carbonDrumDaysAllowed { get; private set; }
        public int? carbonDrumWeightAllowed { get; private set; }
        public bool batchManaged { get; private set; }
        public bool isRawMaterial { get; private set; }
        public bool processOrderRequired { get; private set; }
        public int sequenceIdStart { get; private set; }
        public int? sequenceIdEnd { get; private set; }
        public string vendor { get; private set; }
        public int? currentId { get; private set; }

        public Materials getMaterial(int input)
        {

            return (from materialNumber in context.materialNumbers
                    join material in context.materials on materialNumber.materialNameId equals material.materialNameId
                    join materialId in context.materialIds on materialNumber.materialNumber1 equals materialId.materialNumber
                    join productNumberSequence in context.productNumberSequences on materialId.sequenceId equals productNumberSequence.sequenceId
                    where materialNumber.materialNumber1 == input
                    select new Materials
                    {
                        number = materialNumber.materialNumber1,
                        nameId = material.materialNameId,
                        name = material.materialName,
                        nameAbreviation = material.materialNameAbreviation,
                        productCode = material.productCode,
                        rawMaterialCode = material.rawMaterialCode,
                        permitNumber = material.permitNumber,
                        unitOfIssue = materialNumber.unitOfIssue,
                        carbonDrumRequired = material.carbonDrumRequired,
                        carbonDrumDaysAllowed = material.carbonDrumDaysAllowed,
                        carbonDrumWeightAllowed = material.carbonDrumWeightAllowed,
                        batchManaged = materialNumber.batchManaged,
                        isRawMaterial = materialNumber.isRawMaterial,
                        processOrderRequired = materialNumber.requiresProcessOrder,
                        currentId = materialId.currentSequenceId,
                        sequenceIdStart = productNumberSequence.sequenceIdStart,
                        sequenceIdEnd = productNumberSequence.sequenceIdEnd,
                    }).First();
        }

        internal List<Materials> getRawMaterialFromDatabase(int nameId)
        {
            return (from materialNumber in context.materialNumbers
                    join material in context.materials on materialNumber.materialNameId equals material.materialNameId
                    join materialId in context.materialIds on materialNumber.materialNumber1 equals materialId.materialNumber
                    join vendor in context.vendors on materialId.vendorId equals vendor.vendorId
                    join productNumberSequence in context.productNumberSequences on materialId.sequenceId equals productNumberSequence.sequenceId
                    where materialNumber.materialNameId == nameId && materialNumber.isRawMaterial == true
                    select new Materials
                    {
                        number = materialNumber.materialNumber1,
                        vendor = vendor.vendorName,
                        sequenceIdStart = productNumberSequence.sequenceIdStart,
                        sequenceIdEnd = productNumberSequence.sequenceIdEnd,
                        currentId = materialId.currentSequenceId,
                        rawMaterialCode = material.rawMaterialCode

                    }).ToList();
        }

    }
}
