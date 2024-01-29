
using Shared.DTO;
using AutoMapper;
using Service;
using Service.Contracts;
using Service.Tests.Fakes;
using RavenDB.Exceptions;
using RavenDB.Models;
using LoggerService;

namespace RavenBAL.Tests
{
    public class MaterialVendorServicesTests
    {
        private readonly IRepoManager _repo = Substitute.For<IRepoManager>();
        private readonly MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<MaterialVendor, MaterialVendorDTO>();
            cfg.CreateMap<MaterialVendor, MaterialVendorWithRawMaterialDTO>();
            cfg.CreateMap<RawMaterial, RawMaterialDTO>();
        });
        private readonly ILoggerManager _log = new LoggerManager();
        private RavenContextFakeBuilder _ctx = new();
        private IServiceManager _sut;
        public MaterialVendorServicesTests()
        {
        }

        [Fact]
        public async void GetMaterialVendor_MaterialNotFound_MaterialNotFoundException()
        {
            //Arrange
            int materialNumber = 0;
            _sut = new ServiceManager(_repo, null, null);
            //Act
            var e = await Record.ExceptionAsync(() => _sut.MaterialVendorServices.GetMaterialVendor(materialNumber));
            //Assert
            var ex = Assert.IsType<MaterialNotFoundException>(e);
            Assert.StartsWith("The material number", ex.Message);
        }
        [Fact]
        public async void GetMaterialVendor_ReturnMaterialVendor()
        {
            //Arrange
            var _mapper = config.CreateMapper();
            int materialNumber = 3282571;
            var vendor = _ctx.WithMaterialVendorMilk();
            _repo.MaterialVendor.GetMaterialVendor(materialNumber).Returns(vendor);
            _sut = new ServiceManager(_repo, null, _mapper);

            //Act
            var mv = await _sut.MaterialVendorServices.GetMaterialVendor(materialNumber);

            //Assert
            Assert.IsType<MaterialVendorDTO>(mv);
            Assert.Equal(materialNumber, mv.MaterialNumber);
        }
        [Fact]
        public async void GetApprovedRawMaterial_ReturnListOfApprovedRawMaterial()
        {
            //Arrange
            var expected = 1;
            var parentMaterialNumber = 58971;
            var _mapper = config.CreateMapper();
            var vendor = _ctx.WithListMaterialVendorMilk();
            _repo.MaterialVendor.GetMaterialVendorWithRawMaterial(parentMaterialNumber).Returns(vendor);
            _sut = new ServiceManager(_repo, null, _mapper);
            //Act
            var actual = await _sut.MaterialVendorServices.GetApprovedRawMaterial(parentMaterialNumber);
            //Assert
            Assert.Equal(expected, actual.Count());
        }
    }
}