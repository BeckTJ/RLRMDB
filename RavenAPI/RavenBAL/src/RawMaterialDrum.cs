using Contracts;
using Shared.DTO;

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
                DrumLotNumber = null,
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
