using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace RLRMWF
{
    public class RawMaterialLog
    {
        RLRMDBEntities context = new RLRMDBEntities();

        //Raw Material
        public string drumId { get; private set; }
        public int materialNumber { get; set; }
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


        internal void setRawMaterialDrum(int materialNumber, string vendor1, string vendorBatch, string distilOperator,
            long inspectionLotNumber, int sapBatch, string containerNumber, int netWeight, string sampleNumber)
        {
            this.materialNumber = materialNumber;
            this.vendorBatch = vendorBatch;
            this.sapBatch = sapBatch;
            this.netWeight = netWeight;
            this.sampleNumber = sampleNumber;
            ctn = containerNumber;
            distillationOperator = distilOperator;
            inspectionLot = inspectionLotNumber;
            vendor = vendor1;
        }
        internal void addDrumToDatabase()
        {
            context.rawMaterialUpdate(materialNumber, vendor, vendorBatch, netWeight, sapBatch, ctn, processOrder, drumQuantity);
            
        }

        internal void getDrumId(object number, object name)
        {
        }
        internal List<Vendors> getVendorList()
        {
            List<Vendors> vendorList = new List<Vendors>();
            var nameId = materials.getMaterialNameId(number);
            var rawmat = materials.getRawMaterialFromDatabase(nameId);

            var vendorlist = vendorName.getVendorFromDatabaseByMaterialNameId(rawmat[i].number);
            return vendorList;

        }
    }
}