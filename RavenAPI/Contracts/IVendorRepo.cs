using RavenDB.Models;

namespace Contracts
{
    public interface IVendorRepo : IRepoBase<VendorLot>
    {
        IEnumerable<VendorLot> GetAllVendors(); 
        VendorLot GetVendorByVendorLot(string lotNumber);
        IEnumerable<VendorLot> GetVendorLotsWithRawMaterials(int materialNumber);
        void SubmitVendorLot(VendorLot vendorLot);

    }
}
 