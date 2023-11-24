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
            CreateMap<VendorLot, VendorLotDTO>();
            CreateMap<RawMaterial, RawMaterialDTO>();
            CreateMap<SampleSubmit, SampleDTO>();
            CreateMap<CreateRawMaterialDTO, RawMaterial>();

        }
    }
}
/*
 * CreateMap<Company, CompanyDTO>()
 *      .ForCtorParam("FullAddress",
 *          opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));
 */