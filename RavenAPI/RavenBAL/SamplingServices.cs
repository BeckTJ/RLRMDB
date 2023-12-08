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
        public void SubmitSample(string sampleId) 
        {
            _repo.SampleRepo.SubmitSample(_mapper.Map<SampleSubmit>(new SampleSubmitDTO
            {
                SampleSubmitNumber = sampleId,
                SampleDate = DateTime.Now,
            }));
        }
        public IEnumerable<RequiredSampleDTO> VerifySampleRequired(int parentMaterialNumber)
        {
            var sample = _repo.SampleRequired.GetSampleRequired(parentMaterialNumber);
            return _mapper.Map<IEnumerable<RequiredSampleDTO>>(sample);
        }
    }
}
