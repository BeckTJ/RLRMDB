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
                .ForMember(x => x.Amp, opt => opt.MapFrom(s => 
                new SampleContainer
                {
                    ContainerType = "Amp",
                    SampleSize = (int)s.AmpVolume,
                    Quantity = (int)s.Amps,
                    UnitOfIssue = s.AmpUnitOfIssue,
                }))
                .ForMember(x => x.MetalBubbler, opt => opt.MapFrom(s => 
                new SampleContainer
                {
                    ContainerType = "Metal Bubbler",
                    SampleSize = (int)s.BubblerVolume,
                    Quantity = (int)s.MetalBubbler,
                    UnitOfIssue = s.BubblerUnitOfIssue,
                }))
                .ForMember(x => x.Vial, opt => opt.MapFrom(s => 
                new SampleContainer
                {
                    ContainerType = "Vial",
                    SampleSize = (int)s.VialVolume,
                    Quantity = (int)s.Vials,
                    UnitOfIssue = s.VialUnitOfIssue,
                }));

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