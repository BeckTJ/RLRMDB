using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLRMWF
{

    public class Vendors
    {
        RLRMDBEntities context = new RLRMDBEntities();


        
        public int getVendorIdFromDatabase(string vendorName)
        {
            return context.vendors
                .Where(v => v.vendorName == vendorName)
                .Select(v => v.vendorId).FirstOrDefault();
        }

        public List<string> getVendorFromDatabase(int matNum)
        {
            return (from vendor in context.vendors
                    join materialId in context.materialIds on vendor.vendorId equals materialId.vendorId
                    join materialNumber in context.materialNumbers on materialId.materialNumber equals materialNumber.materialNumber1
                    where materialNumber.materialNumber1 == matNum
                    select vendor.vendorName).ToList();
        }
    }
}
