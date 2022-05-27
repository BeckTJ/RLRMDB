using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLRMWF
{
    public class QualityControl
    {
        RLRMDBEntities context = new RLRMDBEntities();

        public string sampleNumber { get; private set; }
        public DateTime? rejectedDate { get; private set; }
        public bool rejected { get; private set; }
        public DateTime? approvalDate { get; private set; }
        public DateTime? expDate { get; private set; }
        public string qcOperator { get; private set; }

        internal void verifySampleSubmit(string sample)
        {
            qcOperator = "JJ";

            using (RLRMDBEntities context = new RLRMDBEntities())
            {
                if (context.SampleSubmits.Any(qc => qc.SampleSubmitNumber == sample)) return;
                {
                    SampleSubmit control = new SampleSubmit();
                    sampleNumber = control.SampleSubmitNumber;
                    rejectedDate = control.RejectedDate;
                    rejected = (bool)control.Rejected;
                    approvalDate = control.ApprovalDate;
                    expDate = control.ExperiationDate;
                }
            }
        }
        public QualityControl getSampleInfo(string sample)
        {
            using(RLRMDBEntities context = new RLRMDBEntities())
            {
                return (from qualityControl in context.SampleSubmits
                where qualityControl.SampleSubmitNumber == sample
                select new QualityControl
                {
                    sampleNumber = qualityControl.SampleSubmitNumber, 
                    rejected = (bool)qualityControl.Rejected,
                    rejectedDate = qualityControl.RejectedDate,
                    approvalDate = qualityControl.ApprovalDate,
                    expDate = qualityControl.ExperiationDate
                }).FirstOrDefault();
            }
        }
            
    }
}
