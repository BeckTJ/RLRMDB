using AutoMapper;
using RavenDB.Exceptions;
using RavenDB.Models;
using Service.Repo;
using Service.Repo.Contracts;
using Service.Tests.Unit.Fakes;
using Shared.DTO;

namespace Service.Tests.Unit
{
    public class RequiredSampleTests
    {
        private readonly IRepoManager _repo = Substitute.For<IRepoManager>();
        private readonly RavenContextFakeSampleRequired _ctxSampleRequired = new();
        private readonly RavenContextFakeMaterialVendor _ctxMaterialVendor = new();
        private readonly MapperConfiguration _config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<SampleRequired, SampleRequiredDTO>();
            cfg.CreateMap<MaterialVendor, MaterialVendorDTO>();
        });
        private IServiceRepoManager _sut;

        [Fact]
        public async void GetRequiredSample_NoSampleData_SampleDataNotFoundException()
        {
            //Arrange
            MaterialVendorDTO material = null;
            _sut = new ServiceRepoManager(_repo,null,null);
            
            //Act
            var e = await Record.ExceptionAsync(() => _sut.QualityControl.CheckRequiredSample(material));

            //Assert
            var ex = Assert.IsType<SampleDataNotFoundException>(e);
            Assert.StartsWith("No Sample Data Found", ex.Message);
        }

        [Fact]
        public async void GetRequiredSample_NewVendorLot_OneDrumSample()
        {
            //Arrange
            int materialNumber = 58423;
            var material = _ctxMaterialVendor.WithMaterialVendorJuice();
            var _map = _config.CreateMapper();
            var sampleRequired = _ctxSampleRequired.WithSampleRequiredJuice();
            _repo.SampleRequired.GetSampleRequired(materialNumber).Returns(sampleRequired);
            _sut = new ServiceRepoManager(_repo, null, _map);

            //Act
            var actual = await _sut.QualityControl.CheckRequiredSample(material);

            //Assert
            Assert.Equal(1, actual.Count());

        }

        [Fact]
        public async void GetRequiredSample_OldVendorLot_NoDrumSample()
        {
            //Arrange
            
            int materialNumber = 58423;
            var sampleRequired = _ctx.WithSampleRequiredJuice();
            //Act
            //Assert
        }

        [Fact]
        public async void GetRequiredSample_NewVendorLot_AllDrumsSampled()
        {
            //Arrange
            int materialNumber = 58245;
            var sampleRequired = _ctx.WithSampleRequiredBeer();
            
            //Act
            //Assert
        }

        [Fact]
        public async void GetRequiredSample_OldVendorLot_AllDrumsSampled()
        {
            //Arrange
            int materialNumber = 58245;
            var sampleRequired = _ctx.WithSampleRequiredBeer();

            //Act
            //Assert
        }
    }
}
