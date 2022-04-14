using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLRMWF
{
    public class VendorBatch
    {
        RLRMDBEntities context = new RLRMDBEntities();

        public string vendorName { get; private set; } 
        public string vendorBatchNumber { get; private set; }
        public int? quantity { get; private set; }

        public List<VendorBatch> getVendorBatch(int materialNumber, string vendorName)
        {
           return(from vendorBatchInformation in context.vendorBatchInformations
                    join vendor in context.vendors on vendorBatchInformation.vendorId equals vendor.vendorId
                    where vendorBatchInformation.materialNumber == materialNumber && vendor.vendorName == vendorName
                    select new VendorBatch
                    {
                        vendorBatchNumber = vendorBatchInformation.vendorBatchNumber,
                        quantity = vendorBatchInformation.quantity
                    }).ToList();
           


        }
        public List<VendorBatch> getVendorBatchFromDatabase(int materialNumber)
        {

            return (from vendorBatchInformation in context.vendorBatchInformations
                    join vendor in context.vendors on vendorBatchInformation.vendorId equals vendor.vendorId
                    where vendorBatchInformation.materialNumber == materialNumber && vendorBatchInformation.quantity > 0
                    group vendor by vendor.vendorName into Vendors
                    orderby Vendors
                    select new VendorBatch
                   {
                   }).ToList();
                    

            
        }
    }
}

