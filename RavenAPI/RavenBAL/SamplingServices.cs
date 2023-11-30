using AutoMapper;
using Contracts;
using RavenDB.Models;
using Service.Contracts;
using Shared.DTO;

namespace Service
{
    internal sealed class SamplingServices : ISamplingServices
    {
        private readonly IRepoManager _repo;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        public SamplingServices(IRepoManager repo, ILoggerManager log, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = log;
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

        public IEnumerable<RequiredSampleDTO> VerifySampleRequired(int parentMaterialNumber)
        {
            var sample = _repo.SampleRequired.GetSampleRequired(parentMaterialNumber);
                //.GroupBy(s => s.MaterialType).Select(grp => grp.ToList()).ToList();
            return _mapper.Map<IEnumerable<RequiredSampleDTO>>(sample);
        }
    }
}
