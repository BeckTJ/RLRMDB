
using AutoMapper;
using Contracts;
using RavenDB.Exceptions;
using RavenDB.Models;
using Service.Contracts;
using Shared.DTO;

namespace Service.src
{
    internal sealed class RawMaterialDrum
    {
        private readonly IRepoManager _repo;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public RawMaterialDrum(IRepoManager repo, ILoggerManager log, IMapper mapper)
        {
            _repo = repo;
            _logger = log;
            _mapper = mapper;
        }

        public IEnumerable<RawMaterialDTO> GetAllRawMaterial()
        {
            var material = _repo.RawMaterial.GetAllRawMaterial();
            var rawMaterial = _mapper.Map<IEnumerable<RawMaterialDTO>>(material);
            return rawMaterial;
        }
        public IEnumerable<RawMaterialDTO> GetRawMaterialByMaterialNumber(int materialNumber)
        {
            var rawMaterial = _repo.RawMaterial.GetRawMaterialByMaterialNumber(materialNumber);
            return _mapper.Map<IEnumerable<RawMaterialDTO>>(rawMaterial);
        }
        public RawMaterialDTO GetRawMaterialByProductId(string productId)
        {
            var rawMaterial = _repo.RawMaterial.GetRawMaterialByProductId(productId);
            return _mapper.Map<RawMaterialDTO>(rawMaterial);
        }
        public IEnumerable<RawMaterialDTO> VerifyRawMaterialSample(List<RawMaterialDTO> rawMaterial)
        {
            List<RawMaterialDTO> approved = new List<RawMaterialDTO>();
            Sampling sampling = new Sampling(_repo,_logger,_mapper);

            foreach (var material in rawMaterial)
            {
                //create error check for not sampled
                    approved.Add(material);
            }
            return approved;
        }
        public IEnumerable<RawMaterialDTO> CreateListOfDrums(CreateRawMaterialDTO createRawMaterial)
        {
            Sampling sample = new(_repo, _logger, _mapper);
            List<RawMaterialDTO> rawMaterialDrum = new();

            for (int i = 0; i < createRawMaterial.Quantity; i++)
            {
                //sample.SubmitSample(createRawMaterial.ProductIds.);

                var drum = CreateRawMaterialDrum(createRawMaterial);
                rawMaterialDrum.Add(drum);

                _repo.RawMaterial.CreateRawMaterial(_mapper.Map<RawMaterial>(rawMaterialDrum));
            }
            return rawMaterialDrum;
        }
        public RawMaterialDTO CreateRawMaterialDrum(CreateRawMaterialDTO rawMaterial)
        {
            ProductLotNumber productId = new ProductLotNumber(_repo,_logger,_mapper);

            return new RawMaterialDTO
            {
                DrumLotNumber = productId.UpdateProductLotNumber(productId.CreateProductLotNumber(rawMaterial.MaterialNumber, "Raw")),
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
