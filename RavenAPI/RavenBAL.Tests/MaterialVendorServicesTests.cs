
using Shared.DTO;
using AutoMapper;
using Service;
using Service.Contracts;
using Service.Tests.Fakes;
using RavenDB.Exceptions;
using RavenDB.Models;

namespace RavenBAL.Tests
{
    public class MaterialVendorServicesTests
    {
        private readonly IRepoManager _repo = Substitute.For<IRepoManager>();
        private readonly IMapper _mapper = Substitute.For<IMapper>();
        private RavenContextFakeBuilder _ctx = new();
        private IServiceManager _sut;
        public MaterialVendorServicesTests()
        {
        }

        [Fact]
        public void GetMaterialVendor_MaterialNotFound_MaterialNotFoundException()
        {
            //Arrange
            int materialNumber = 0;
            _sut = new ServiceManager(_repo, null, null);
            //Act
            var e = Record.Exception(() => _sut.MaterialVendorServices.GetMaterialVendor(materialNumber));
            //Assert
            var ex = Assert.IsType<MaterialNotFoundException>(e);
            Assert.StartsWith("The material number", ex.Message);
        }
        [Fact]
        public void GetMaterialVendor_ReturnMaterialVendor()
        {
            //Arrange
            MaterialVendorDTO mv = new();
            int materialNumber = 3282571;
            var vendor = _ctx.WithMaterialVendorMilk();
            _repo.MaterialVendor.GetMaterialVendor(materialNumber).Returns(vendor);
            _mapper.Map(vendor, mv).Returns(new MaterialVendorDTO());
            _sut = new ServiceManager(_repo, null, _mapper);

            //Act
            mv = _sut.MaterialVendorServices.GetMaterialVendor(materialNumber);
            Console.WriteLine(mv);
            //Assert
            Assert.Equal(materialNumber, mv.MaterialNumber);
        }
    }
}