using RavenDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IMaterialVendorRepo:IRepoBase<MaterialVendor>
    {
        MaterialVendor GetMaterialVendor(int materialNumber);
        IEnumerable<MaterialVendor> GetMaterialVendorsWithVendorLot(int materialNumber);
    }
}
