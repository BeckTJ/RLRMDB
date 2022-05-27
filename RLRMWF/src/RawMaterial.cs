using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLRMWF
{
    internal class RawMaterial
    {RLRMDBEntities context = new RLRMDBEntities();
        public string drumId { get; set; }
        public RawMaterial()
        {
        }
        //need to add operator to raw material log in database
        internal RawMaterial setNewDrumInput(int rawMaterialNumber, string vendorName, string vendorBatch, string distillationOperator)
        {
            int? netWeigth = null;
            int? sapBatch = null;
            string ctn = null;
            int? qty = null; //need to add qty to user input for vendor lots

            var index = context.RawMaterialUpdate(rawMaterialNumber, vendorName, vendorBatch, netWeigth, sapBatch, ctn, qty);
            
            return  (from RawMaterial in context.RawMaterialLogs
                     join MaterialId in context.MaterialIds on RawMaterial.MaterialNumber equals MaterialId.MaterialNumber
                     where MaterialId.MaterialNumber == rawMaterialNumber
                     orderby RawMaterial.DrumLotNumber descending
                     select new RawMaterial{
                        drumId = RawMaterial.DrumLotNumber
                     }).Take(1).FirstOrDefault();
        }

        internal void setNewDrumInput(int rawMaterialNumber, string vendor, long inspectionLot, int sapBatch, string ctn, int netWeight, string distillationOperator)
        {
            string vendorBatch = null;
            int? qty = null;
         
            context.RawMaterialUpdate(rawMaterialNumber, vendor, vendorBatch, netWeight, sapBatch, ctn, qty);
        }
        
    }
}