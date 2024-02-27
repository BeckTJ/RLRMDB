using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repo.Contracts
{
    public interface IVendor
    {
        Task<MaterialVendorDTO> GetApprovedRawMaterial(int parentMaterialNumber);
        Task<MaterialVendorDTO> GetMaterialVendor(int materialNumber);
        void InputVendorLot(CreateRawMaterialDTO material);
    }
}
