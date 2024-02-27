using Service.Tests.Fakes;
using Service.Contracts;
using AutoMapper;
using Shared.DTO;
using RavenDB.Models;

namespace Service.Tests.Unit
{
    public class RawMaterialDrumTests
    {
        private readonly IRepoManager _repo = Substitute.For<IRepoManager>();
        private readonly MapperConfiguration _config = new(cfg =>
        {
            cfg.CreateMap<RawMaterial, RawMaterialDrumDTO>();
        });
        
        private IServiceManager _sut;
        private RavenContextFakeBuilder _ctx = new();

        [Fact]
        public void GetApprovedRawMaterial_ReturnListofRawMaterial()
        {
            //Arrange
            int materialNumber = 58971;
            var expected = 1;

            //Act

            //Assert
        }
    }
}
