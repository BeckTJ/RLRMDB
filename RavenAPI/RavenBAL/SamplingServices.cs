using AutoMapper;
using Contracts;
using RavenDB.Models;
using Shared.DTO;

namespace Service
{
    internal sealed class SamplingServices
    {
        private readonly IRepoManager _repo;
        public SamplingServices(IRepoManager repo)
        {
            _repo = repo;
        }
        /*
         * Check if material needs to be sampled
         *      -> If material is expired
         *         if vendor lot old new or reclaim
         *         
         */
        public bool VerifyProductSample(RawMaterialDTO rawMaterial)
        {
            return true;
        }
        private bool VerifyExpDate(string sampleId)
        {
            return true;
        }

        private void VerifySampleRequired(int parentMaterialNumber, string materialType)
        {
            var sample = _repo.SampleRequired.GetSampleRequired(parentMaterialNumber)
                .GroupBy(s => s.MaterialType).Select(grp => grp.ToList()).ToList();

        }
    }
}
