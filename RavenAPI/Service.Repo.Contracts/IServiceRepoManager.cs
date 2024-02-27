using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repo.Contracts
{
    public interface IServiceRepoManager
    {
        IRawMaterialDrum RawMaterialDrum { get; }
        IHighPurityMaterial HighPurityMaterial { get; }
        IVendor Vendor { get; }
        IQualityControl QualityControl { get; }
    }
}
