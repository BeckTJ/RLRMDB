
namespace RavenDB.Exceptions
{
    public sealed class VendorLotNotFoundException : NotFoundException
    {
        public VendorLotNotFoundException(string vendorLot)
        : base($"Vendor Lot: {vendorLot} does not exsist. Please Sample.")
        { }
    }
}
