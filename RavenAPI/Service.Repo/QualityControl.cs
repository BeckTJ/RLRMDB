using AutoMapper;
using Contracts;
using RavenDB.Exceptions;
using Service.Repo.Contracts;
using Shared.DTO;

namespace Service.Repo
{
    internal sealed class QualityControl : IQualityControl    
    {
        private readonly IRepoManager _repo;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public QualityControl(IRepoManager repo, ILoggerManager logger, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
        }
        /*
         * check vendor name -> if reclaim return sample info
         * else
         * check if material has been sampled. If it has check if material requires old vendor lots to be sampled.
         */
        public async Task<IEnumerable<RequiredSampleDTO>> CheckRequiredSample(MaterialVendorDTO material)
        {
            string materialType;

            if (material.VendorName == "Reclaim")
            {
                materialType = "Reclaim";                  
            }
            else
            {
                materialType = "Raw Material";
            }
            var required = await GetRequiredSample(material.ParentMaterialNumber, materialType);

            if (required is null)
                throw new SampleDataNotFoundException(material.ParentMaterialNumber);
            //check the to see if material has been sampled
            return required;
        }
        public async Task<IEnumerable<RequiredSampleDTO>> GetRequiredSample(int parentMaterialNumber,string materialType)
        {
            /*
             * Check if sample is needed
             */
            List<RequiredSampleDTO> result = new();
            var required = await _repo.SampleRequired.GetSampleRequired(parentMaterialNumber);
            if (required is null)
                throw new SampleDataNotFoundException(parentMaterialNumber);

            foreach (var item in required)
            {
                if(item.MaterialType == materialType)
                {
                    var requiredSample = _mapper.Map<RequiredSampleDTO>(item);

                    result.Add(requiredSample);
                }
            }
            
            return result;
        }
        public void SubmitSample(string sampleType, long inspectionLotNumber)
        {
            throw new NotImplementedException();
        }
    }
}
