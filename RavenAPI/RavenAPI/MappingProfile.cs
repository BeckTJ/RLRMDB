using AutoMapper;
using RavenDAL.DTO;
using RavenBAL.DTO;
using RavenDAL.Models;

namespace RavenAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            //DAL
            CreateMap<Material, MaterialDTO>();
            CreateMap<VendorLot, MaterialVendorDTO>();
            CreateMap<RawMaterial, RawMaterialDTO>();
            CreateMap<SampleSubmit, SampleDTO>();
            CreateMap<CreateRawMaterialDTO, RawMaterial>();

            //BAL
            CreateMap<SampleRequired, CheckSampleDTO>();

        }
    }
}
