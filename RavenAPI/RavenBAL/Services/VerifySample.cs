using Contracts;
using RavenDAL.DTO;

namespace RavenBAL.Services
{
    public class VerifySample
    {
        private IRepoWrapper _repo;
        public VerifySample(IRepoWrapper repo) 
        {
            _repo = repo;
        }
        //
        public bool VerifyProductSample(RawMaterialDTO rawMaterial)
        {
            return true;
        }
        private bool VerifyExpDate(string sampleId)
        {
            return true;
        }

        private bool VerifySampleRequired(int materialNumber)
        {
            var material = _repo.Material.GetParentMaterialNumberFromChild(materialNumber);
            var sample = _repo.SampleRequired.VerifySampleVLN(material.MaterialNumber);

            if (sample != null)
            {
                
                    return true;
            }
            return false;
        }
    }
}