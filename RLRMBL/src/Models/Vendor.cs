using System;
using System.Collections.Generic;
using RLRMBL.Context;

namespace RLRMBL.Models
{
    public partial class Vendor
    {
        RLRMDBContext context = new RLRMDBContext();
        public Vendor()
        {
            RawMaterials = new HashSet<RawMaterial>();
            VendorBatchInformations = new HashSet<VendorBatchInformation>();
        }

        public int VendorId { get; set; }
        public string VendorName { get; set; } = null!;

        public virtual ICollection<RawMaterial> RawMaterials { get; set; }
        public virtual ICollection<VendorBatchInformation> VendorBatchInformations { get; set; }

        internal Vendor getVendor(string input)
        {
            return (from Vendor in context.Vendors
                    where Vendor.VendorName == input
                    select new Vendor
                    {
                        VendorId = Vendor.VendorId,
                        VendorName = Vendor.VendorName
                    }).First();
        }

        internal void addVendor(string vendor)
        {
            Vendor v = new Vendor();
            v.VendorName = vendor;

            context.Add(v);
            context.SaveChanges();
        }
    }
}
