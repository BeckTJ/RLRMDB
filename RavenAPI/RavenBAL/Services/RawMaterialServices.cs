
using Contracts;
using RavenDAL.DTO;

namespace RavenBAL.Services
{
    public class RawMaterialServices
    {
        private IRepoWrapper _repo;

        /*
         * Create a new drum
         * sample drum (single and Multiple)
         * select a drum for Lot
         */

        public RawMaterialServices() { }
        public void CreateRawMaterialDrum(CreateRawMaterialDTO rawMaterial)
        {
            var material = _repo.Material.GetMaterialByMaterialNumber(rawMaterial.MaterialNumber);

        }
        public void SampleRawMaterial(RawMaterialDTO rawMaterial) { }

        public void GetProductId(int materialNumber)
        {
            var material = _repo.Material.GetMaterialByMaterialNumber(materialNumber);
        }
    }
}
