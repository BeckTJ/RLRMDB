using AutoMapper;
using Contracts;
using Shared.DTO;

namespace Service.src
{
    public class ProductLotNumber 
    {
        private readonly IRepoManager _repo;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public ProductLotNumber(IRepoManager repo, ILoggerManager log, IMapper mapper)
        {
            _repo = repo;
            _logger = log;
            _mapper = mapper;
        }

        public string CreateProductLotNumber(int materialNumber, string materialType)
        {
            ProductLotNumberDTO material = new();
            string productId = null;
            string lastProductId = null;

            if(materialType == "Raw")
            {
                MaterialVendorServices materialVendor = new(_repo, _logger, _mapper);
                material = materialVendor.GetMaterialVendorForProductId(materialNumber);
                
                RawMaterialDrum rawMaterialDrum = new(_repo,_logger,_mapper);
                lastProductId = rawMaterialDrum.GetRawMaterialByMaterialNumber(materialNumber)
                    .OrderByDescending(x => x.DrumLotNumber).FirstOrDefault().DrumLotNumber;
            }
            else
            {
                //Create product id for Material
                MaterialServices materialService = new(_repo, _logger, _mapper);
            }

            if (lastProductId != null)
            {
                var id = GetNextSequenceId(lastProductId);
                productId = id + material.MaterialCode;
            }
            else
            {
                productId = material.SequenceId + material.MaterialCode;
            }
            return productId;
        }

        private int GetNextSequenceId(string productId)
        {
            if (productId.Length == 10 || productId.Length == 6)
            {
                return int.Parse(productId[..4]) + 1;
            }
            else
            {
                return int.Parse(productId[..3]) + 1;
            }
        }

        public string UpdateProductLotNumber(string lotNumber)
        {
            var todaysDate = DateTime.Today;
            var today = todaysDate.ToString("MM");
            var dateCode = _repo.DateCode.GetDateCode(int.Parse(today)).AlphabeticCode;
            var year = todaysDate.Year % 10;
            var day = todaysDate.ToString("dd");

            return lotNumber + year + dateCode + day;
        }
    }
}
