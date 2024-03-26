using RavenDB.Models;

namespace Contracts
{
    public interface IVendorRepo : IRepoBase<VendorLot>
    {
        Task<IEnumerable<VendorLot>> GetAllVendors(); 
        Task<VendorLot> GetVendorByVendorLot(string lotNumber);
        Task<IEnumerable<VendorLot>> GetVendorLotsWithRawMaterials(int materialNumber);
        void SubmitVendorLot(VendorLot vendorLot);
        void UpdateVendorLot(VendorLot vendorLot);
    }
}
 