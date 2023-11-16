using AutoMapper;
using Contracts;
using RavenDAL.DTO;
using RavenDAL.Models;

namespace RavenBAL.Services
{
    public class VerifySample
    {
        private IRepoWrapper _repo;
        private IMapper _mapper;
        public VerifySample(IRepoWrapper repo, IMapper mapper) 
        {
            _repo = repo;
            _mapper = mapper;
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

        private SampleRequiredDTO VerifySampleRequired(int parentMaterialNumber, string materialType)
        {
            var sample = _repo.SampleRequired.VerifySampleVLN(parentMaterialNumber)
                .GroupBy(s => s.MaterialType).Select(grp => grp.ToList()).ToList();

            return _mapper.Map<SampleRequiredDTO>(sample);
        }
    }
}
