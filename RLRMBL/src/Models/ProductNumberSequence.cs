using System;
using System.Collections.Generic;
using RLRMBL.Context;

namespace RLRMBL.Models
{
    public partial class ProductNumberSequence
    {
        public int SequenceId { get; set; }
        public int SequenceIdStart { get; set; }
        public int SequenceIdEnd { get; set; }
        RLRMDBContext context = new RLRMDBContext();


        internal ProductNumberSequence getSequenceId(int sequenceIdStart)
        {

            return (from ProductNumberSequence in context.ProductNumberSequences
                    where ProductNumberSequence.SequenceIdStart == sequenceIdStart
                    select new ProductNumberSequence
                    {
                        SequenceId = ProductNumberSequence.SequenceId
                    }).First();
        }
    }
}
