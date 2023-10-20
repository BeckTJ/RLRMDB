using RavenDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IVendorRepo : IRepoBase<VendorLot>
    {
        IEnumerable<VendorLot> GetAllVendors(); 
        VendorLot GetVendorByVendorLot(string lotNumber);
        IEnumerable<VendorLot> GetVendorLotsWithRawMaterials(int materialNumber);

    }
}
 