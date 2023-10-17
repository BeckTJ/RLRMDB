using RavenDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IVendorRepo : IRepoBase<VendorBatch>
    {
        IEnumerable<VendorBatch> GetAllVendors(); 
        VendorBatch GetVendorByVendorLot(string lotNumber);
        IEnumerable<VendorBatch> GetVendorLotsWithRawMaterials(int materialNumber);

    }
}
 