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

        public int? materialNumber { get; private set; }
        public string vendorName { get; private set; }
        public bool? isMPPS { get; private set; }

        public int getVendorIdFromDatabase(string vendorName)
        {
            return context.Vendors
                .Where(v => v.VendorName == vendorName)
                .Select(v => v.VendorId).FirstOrDefault();
        }

        public List<Vendors> getVendorFromDatabase(int matNum)
        {
            return (from vendor in context.Vendors
                    join materialId in context.MaterialIds on vendor.VendorId equals materialId.VendorId
                    where materialId.MaterialNumber == matNum
                    select new Vendors
                    {
                        vendorName = vendor.VendorName,
                        materialNumber = materialId.MaterialNumber,
                        isMPPS = vendor.IsMPPS
                    }).ToList();
        }

        public bool getIsMpps(string vendor)
        {
            return (bool)context.Vendors
                .Where(v => v.VendorName == vendor)
                .Select(v => v.IsMPPS).FirstOrDefault();
        }
    }
}
