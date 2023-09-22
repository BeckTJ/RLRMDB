using AutoMapper;
using RavenDAL.DTO;
using RavenDAL.Models;

namespace RavenAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Material, MaterialDTO>();
            CreateMap<RawMaterial, RawMaterialDTO>();
        }
    }
}
