using AutoMapper;
using Contracts;
using RavenDB.Models;
using Service.Contracts;
using Shared.DTO;

namespace Service.src
{
    internal sealed class Sampling
    {
        private readonly IRepoManager _repo;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        public Sampling(IRepoManager repo, ILoggerManager log, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = log;
        }
        public SampleSubmitDTO SubmitSample(string sampleType, string sampleId)
        {
            SampleSubmitDTO sample = new()
            {
                SampleId = CreateSampleId(sampleId),
                sampleType = sampleType,
                SampleDate = DateTime.Now,
            };
            _repo.SampleRepo.SubmitSample(_mapper.Map<SampleSubmit>(sample));
            return sample;
        }
        public IEnumerable<SampleRequiredDTO> VerifySampleRequired(int parentMaterialNumber)
        {
            var sample = _repo.SampleRequired.GetSampleRequired(parentMaterialNumber);
            return _mapper.Map<IEnumerable<SampleRequiredDTO>>(sample);
        }
        private int CreateSampleId(string sampleType)
        {
            var id = _repo.SampleRepo.FindAll()
                .OrderByDescending(s => s.SampleId)
                .FirstOrDefault().SampleId;

            //var sampleId = sampleType + (id + 1);

            return id;
        }
    }
}
