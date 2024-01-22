using Contracts;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.src
{
    public class RawMaterialDrum
    {
        private readonly IRepoManager _repoManager;
        public RawMaterialDrum(IRepoManager repoManager) 
        {
            _repoManager = repoManager;
        }

        public RawMaterialDTO CreateRawMaterialDrum(CreateRawMaterialDTO rawMaterial)
        {
            return new RawMaterialDTO
            {
                DrumLotNumber = _repoManager.RawMaterialLotNumber.CreateProductLotNumber(rawMaterial.MaterialNumber),
                BatchNumber = rawMaterial.BatchNumber,
                InspectionLotNumber = rawMaterial.InspectionLotNumber,
                ContainerNumber = rawMaterial.ContainerNumber,
                DrumWeight = rawMaterial.DrumWeight,
                VendorLotNumber = rawMaterial.VendorLotNumber,
                SampleSubmitNumber = null,
            };
        }

    }
}
