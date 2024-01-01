using AutoMapper;
using Contracts;
using RavenDB.Exceptions;
using Service.Contracts;
using Service.src;
using Shared.DTO;

namespace Service
{
    internal sealed class MaterialVendorServices : IMaterialVendorServices
    {
        private readonly IRepoManager _repo;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public MaterialVendorServices(IRepoManager repo, ILoggerManager logger, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
        }
        public MaterialVendorDTO GetMaterialVendor(int parentMaterialNumber,string vendorName)
        {
            var vendor = _repo.MaterialVendor.GetMaterialVendorFromParent(parentMaterialNumber)
                .Where(x => x.VendorName.Equals(vendorName));
            return _mapper.Map<MaterialVendorDTO>(vendor);
        }
        public IEnumerable<MaterialVendorDTO> GetRawMaterialByMaterialNumber(int parentMaterialNumber)
        {
            var rawMaterial = _repo.RawMaterial.GetRawMaterialByMaterialNumber(parentMaterialNumber);

            if (rawMaterial == null)
                throw new MaterialNotFoundException(parentMaterialNumber);

            return _mapper.Map<IEnumerable<MaterialVendorDTO>>(rawMaterial);
        }
        public IEnumerable<MaterialVendorDTO> GetApprovedRawMaterial(int parentMaterialNumber)
        {
            RawMaterialDrum rawMaterialService = new(_repo,_logger, _mapper);
            var vendor = _repo.MaterialVendor.GetMaterialVendorWithVendorLot(parentMaterialNumber);

            if (vendor == null)
                throw new MaterialNotFoundException(parentMaterialNumber);

            var materialVendor = _mapper.Map<IEnumerable<MaterialVendorDTO>>(vendor);

            foreach (var material in materialVendor)
            {
                if (material.VendorLots != null)
                {
                    foreach (var lot in material.VendorLots)
                    {
                        var rawMaterialList = _repo.RawMaterial.GetRawMaterialByMaterialNumber(material.MaterialNumber)
                            .Where(s => s.VendorLotNumber == lot.VendorLotNumber).ToList();

                        var approved = rawMaterialService.VerifyRawMaterialSample(_mapper.Map<List<RawMaterialDTO>>(rawMaterialList));
                        lot.RawMaterials = approved;
                    }
                }
            }
            return materialVendor;
        }
        //create a Material Vendor DTO for New Vendor Lot
        public MaterialVendorDTO InputRawMaterial(CreateRawMaterialDTO material)
        {
            VendorLot vendorLot = new(_repo,_logger,_mapper);
            var materialVendor = GetMaterialVendor(material.MaterialNumber, material.VendorName);
            var lot = vendorLot.VerifyMaterialVendorLot(material);

            _repo.Save();

            materialVendor.VendorLots.ToList().Add(lot);

            return materialVendor;
        }
        public ProductLotNumberDTO GetMaterialVendorForProductId(int materialNumber)
        {
            var vendor = _repo.MaterialVendor.GetMaterialVendor(materialNumber);
            return _mapper.Map<ProductLotNumberDTO>(vendor);
        }

    }
}
