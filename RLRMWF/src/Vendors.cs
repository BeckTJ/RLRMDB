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
                    where materialId.materialNumber == matNum
                    select vendor.vendorName).ToList();
        }

        internal object getVendorFromDatabaseByMaterialNameId(int nameId)
        {
            throw new NotImplementedException();
        }
    }
}
