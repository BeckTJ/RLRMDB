using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace RLRMWF
{
    public class RMLog
    {
        private List<int> rawMaterialNumbers;

        //Raw Material
        public string drumId { get; private set; }
        public int rawMaterialNumber { get; set; }
        public int sapBatch { get; set; }
        public string ctn { get; set; }
        public int netWeight { get; set; }
        public int processOrder { get; set; }
        
        //User
        public string distillationOperator { get; set; }

        //Quality Control
        public string qcOperator { get; set; }
        public long inspectionLot { get; set; }
        public string sampleNumber { get; set; }
        public DateTime approvalDate { get; set; }
        public DateTime experationDate { get; set; }
        public DateTime sampleDate { get; set; }
        public DateTime rejectedDate { get; set; }
        public bool rejected { get; set; }

        //VendorBatchInformation
        public string vendor { get; set; }
        public string vendorBatch { get; set; }
        public int drumQuantity { get; set; }

        public RMLog(int number)
        {
            Materials materials = new Materials();
            rawMaterialNumbers = materials.getRawMaterialNumber(number);
        }
        internal bool getIsMpps()
        {
            Vendors vendors = new Vendors();
            var isMpps = vendors.getIsMpps(vendor);
            return isMpps;
        }
        
        public List<Vendors> getVendorList() 
        {                                   
            Vendors vendors = new Vendors();
            List<Vendors> vendorList = new List<Vendors>();

            foreach (var n in rawMaterialNumbers)
            {
                var vendor = vendors.getVendorFromDatabase(n);

                foreach(var v in vendor)
                {
                    vendorList.Add(v);
                }
            }
            return vendorList;
        }
        internal List<string> getVendorBatchList(int rmNumber, string vendorName)
        {
            vendorBatch vendorBatch = new vendorBatch();

            vendor = vendorName;
            rawMaterialNumber = rmNumber;

            var batch = vendorBatch.getVendorBatchList(rmNumber, vendorName);
            return batch;
        }
        
        internal string getUser()
        {
            return "TT";
        }
        public void setRawMaterialDrum(int materialNumber, string vendor1, string vendorBatch, string distilOperator,
            long inspectionLotNumber, int sapBatch, string containerNumber, int netWeight, string sampleNumber)
        {
            rawMaterialNumber = materialNumber;
            this.vendorBatch = vendorBatch;
            this.sapBatch = sapBatch;
            this.netWeight = netWeight;
            this.sampleNumber = sampleNumber;
            ctn = containerNumber;
            distillationOperator = distilOperator;
            inspectionLot = inspectionLotNumber;
            vendor = vendor1;
        }
            
        public void checkSampleSubmit(string sample)
        {
            QualityControl qc = new QualityControl();
            qc.verifySampleSubmit(sample);
            var control = qc.getSampleInfo(sample);
            if(control.approvalDate != null)
                approvalDate = (DateTime)control.approvalDate;
            if(control.expDate != null)
                experationDate = (DateTime)control.expDate;
                qcOperator = qc.qcOperator;
            if (control.rejected == true)
            {
                rejected = control.rejected;
                rejectedDate = (DateTime)control.rejectedDate;
            }
        }

        internal void SubmitMPPSMaterial()
        {
            RawMaterial rawMaterial = new RawMaterial();
            rawMaterial.setNewDrumInput(rawMaterialNumber, vendor, inspectionLot, sapBatch, ctn, netWeight, distillationOperator);

        }

        internal string SubmitRawMaterial()
        {
            RawMaterial rawMaterial = new RawMaterial();
            drumId = rawMaterial.setNewDrumInput(rawMaterialNumber, vendor, vendorBatch, distillationOperator).drumId;
            return drumId;
        }
    }
}