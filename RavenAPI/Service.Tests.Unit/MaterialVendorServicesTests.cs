
using Shared.DTO;
using AutoMapper;
using Service;
using Service.Contracts;
using Service.Tests.Fakes;
using RavenDB.Exceptions;
using RavenDB.Models;
using LoggerService;
using Service.Repo.Contracts;
using Service.Repo;

namespace Service.Tests.Unit
{
    public class MaterialVendorServicesTests
    {
        private readonly IRepoManager _repo = Substitute.For<IRepoManager>();
        private readonly MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<MaterialVendor, MaterialVendorDTO>();
            cfg.CreateMap<MaterialVendor, MaterialVendorWithRawMaterialDTO>();
            cfg.CreateMap<RawMaterial, RawMaterialDrumDTO>();
        });
        private readonly ILoggerManager _log = new LoggerManager();
        private RavenContextFakeBuilder _ctx = new();
        private IServiceRepoManager _sut;
        public MaterialVendorServicesTests()
        {
        }

        [Fact]
        public async void GetMaterialVendor_MaterialVendorNotFound_MaterialNotFoundException()
        {
            //Arrange
            int materialNumber = 0;
            _sut = new ServiceRepoManager(_repo, null, null);
            //Act
            var e = await Record.ExceptionAsync(() => _sut.Vendor.GetMaterialVendor(materialNumber));
            //Assert
            var ex = Assert.IsType<MaterialNotFoundException>(e);
            Assert.StartsWith("The material number", ex.Message);
        }
    }
}