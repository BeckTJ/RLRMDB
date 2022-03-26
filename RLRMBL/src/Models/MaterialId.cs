using System;
using System.Collections.Generic;
using RLRMBL.Context;
namespace RLRMBL.Models
{
    public partial class MaterialId
    {
        RLRMDBContext context = new RLRMDBContext();
        public int MaterialId1 { get; set; }
        public int? MaterialNumber { get; set; }
        public int? VendorId { get; set; }
        public int? SequenceId { get; set; }
        public int? CurrentSequenceId { get; set; }

        public virtual MaterialNumber? MaterialNumberNavigation { get; set; }


        internal void setMaterialId(int number, int vendorId, int sequenceId, int currentSequenceId)
        {
            MaterialNumber = number;
            VendorId = vendorId;
            SequenceId = sequenceId;
            CurrentSequenceId = currentSequenceId;
        }
        internal MaterialId getMaterialId(int input)
        {
            return (from MaterialId in context.MaterialIds
                    where MaterialId.MaterialNumber == input
                    select new MaterialId
                    {
                        MaterialId1 = MaterialId.MaterialId1,
                        MaterialNumber = MaterialId.MaterialNumber,
                        VendorId = MaterialId.VendorId,
                        SequenceId = MaterialId.SequenceId,
                        CurrentSequenceId = MaterialId.CurrentSequenceId
                    }).First();
        }
    }
}
