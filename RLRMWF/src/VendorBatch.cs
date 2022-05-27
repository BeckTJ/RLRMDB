using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLRMWF
{
    public class vendorBatch
    {
        RLRMDBEntities context = new RLRMDBEntities();

        public string vendorName { get; private set; } 
        public string vendorBatchNumber { get; private set; }
        public int? quantity { get; private set; }

        public List<vendorBatch> getVendorBatch(int materialNumber, string vendorName)
        {
            return (from VendorBatch in context.VendorBatches
                    join Vendor in context.Vendors on VendorBatch.VendorId equals Vendor.VendorId
                    where VendorBatch.MaterialNumber == materialNumber && Vendor.VendorName == vendorName
                    select new vendorBatch
                    {
                        vendorBatchNumber = VendorBatch.VendorBatchNumber,
                        quantity = VendorBatch.Quantity
                    }).ToList();
        }
        public List<string> getVendorBatchList(int number, string vendorName)
        {
            Vendors vendors = new Vendors();
            var vendorId = vendors.getVendorIdFromDatabase(vendorName);
            return context.VendorBatches
                .Where(vb => vb.VendorId == vendorId)
                .Where(vb => vb.MaterialNumber == number)
                .Select(vb => vb.VendorBatchNumber).ToList();
        }
    }
}

