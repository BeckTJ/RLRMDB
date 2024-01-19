
using Shared.DTO;
using AutoMapper;
using Service;
using RavenDB.Exceptions;

namespace RavenBAL.Tests
{
    public class MaterialVendorServicesTests
    {
        private readonly IRepoManager _repo = Substitute.For<IRepoManager>();
        private readonly ILoggerManager _log = Substitute.For<ILoggerManager>();
        public MaterialVendorServicesTests()
        {
        }

        [Fact]
        public void InputRawMaterial_NullMaterialVendor_AgrumentNullExeption()
        {
            //Arrange
            CreateRawMaterialDTO rm = new CreateRawMaterialDTO()
            {
                MaterialNumber = 11111,
            };
            var sut = new ServiceManager(_repo, _log, null);

            //Act
            var e = Record.Exception(() => sut.MaterialVendorServices.InputRawMaterial(rm));

            //Assert
            Assert.IsType<MaterialNotFoundException>(e);
            //Assert.Equal("materialNumber", ex.);
            //Assert.StartsWith("Null", ex.Message);
        }

        /*Comments Are required to process specific raw material*/
        [Theory]
        [InlineData(58971, 3282571, "Stop N Shop", "111-222-333", "Raw12345",null /* 11111*/, null, null, null, 5)]
        [InlineData(58143, 32409, null /*"Wine Palace"*/, "222-333-444", "Raw2345", null, null, null, null, 2)]
        [InlineData(58765, 30173, "Bevmo", "333-444-555", "Raw34567", 22222,null /*"T222333"*/, 55, null, 1)]
        [InlineData(58245, 3948173, "Reclaim", null, "Raw45678", null, null, 180, null /*1111222333*/, 1)]

        public void InputRawMaterial_DataValidation(int parentMaterialNumber, int materialNumber, string vendor, string vendorLot, string sampleId, int batchNumber, string ctn, int drumWeight, long inspectionLot, int qty)
        {
            //Arrange
            var sut = new ServiceManager(null, null, null);

            //Act
            var e = Record.Exception(() => sut.MaterialVendorServices.InputRawMaterial(null!));

            //Assert
            var ex = Assert.IsType<ArgumentOutOfRangeException>(e);
        }
        [Theory]
        [InlineData(58971, 3282571, "Stop N Shop", "111-222-333", "Raw12345", 11111, null, null,null, 5)]
        [InlineData(58143, 32409, "Wine Palace", "222-333-444", "Raw2345", null, null, null, null, 2)]
        [InlineData(58765, 30173, "Bevmo", "333-444-555", "Raw34567", 22222, "T222333", 55, null, 1)]
        [InlineData(58245, 3948173, "Reclaim", null, "Raw45678", null, null, 180, 1111222333, 1)]

        public void InputRawMaterial_DataValidation_PassValidationCheck(int parentMaterialNumber, int materialNumber, string vendor, string vendorLot, string sampleId, int batchNumber, string ctn, int drumWeight, long inspectionLot, int qty)
        {

        }
    }
}