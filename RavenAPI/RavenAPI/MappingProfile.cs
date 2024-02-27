using AutoMapper;
using RavenDB.Models;
using Shared.DTO;

namespace RavenAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Material, MaterialDTO>();
            CreateMap<MaterialVendor, MaterialVendorDTO>();
            CreateMap<MaterialVendor, MaterialVendorWithRawMaterialDTO>();
            CreateMap<VendorLot, VendorLotDTO>();
            CreateMap<VendorLot, VendorLotWithSampleDTO>();
            CreateMap<RawMaterial, RawMaterialDrumDTO>();
            CreateMap<RawMaterial, RawMaterialWithSampleDTO>();
            CreateMap<SampleSubmit, SampleDTO>();
            CreateMap<SampleRequired, SampleRequiredDTO>();
            CreateMap<Material, ProductLotNumberDTO>();
            CreateMap<SampleRequired, RequiredSampleDTO>()
                .ForMember("AmpSampleSize", opt => opt.MapFrom(s => s.AmpVolume + s.AmpUnitOfIssue))
                .ForMember("BubblerSampleSize", opt => opt.MapFrom(s => s.BubblerVolume + s.BubblerUnitOfIssue))
                .ForMember("VialSampleSize", opt => opt.MapFrom(s => s.VialVolume + s.VialUnitOfIssue));

            CreateMap<CreateRawMaterialDTO, RawMaterial>();
            CreateMap<CreateVendorLotDTO, VendorLot>();
            CreateMap<SampleSubmitDTO, SampleSubmit>();
        }
    }
}
/*
 * CreateMap<Company, CompanyDTO>()
 *      .ForCtorParam("FullAddress",
 *          opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));
 */