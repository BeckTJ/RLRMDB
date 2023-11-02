
using RavenDAL.DTO;

namespace RavenBAL.Tests
{
    public class ProductLotNumberTests
    {
        private readonly IProductLotNumber _pln;
        private readonly IRepoWrapper _repo = Substitute.For<IRepoWrapper>();
        private readonly ILoggerManager _loggerManager = Substitute.For<ILoggerManager>();

        private readonly MaterialVendor _material = new MaterialVendor
        {
            MaterialNumber = 123456,
            MaterialCode = "AA",
            SequenceId = 100,
            TotalRecords = 100,
        };
        private readonly IEnumerable<RawMaterial> _rawMaterial = new List<RawMaterial>
        {
            new RawMaterial{ProductId = "100SA", MaterialNumber = 123456, VendorLotNumber = "999-999-999"},
            new RawMaterial{ProductId = "101SA", MaterialNumber = 123456, VendorLotNumber = "999-999-999"},
        };
        private readonly CreateRawMaterialDTO _createRawMaterialDTO = new CreateRawMaterialDTO
        {
            MaterialNumber = 123456,
            VendorLotNumber = "999-999-999",
            SampleId = "Raw45678",
        };

        public ProductLotNumberTests()
        {
            _pln = new ProductLotNumber(_repo);   
        }

        [Fact]
        public void getNextProductLotNumber()
        {
            //Arrange
            var materialNumber = 123456;
            var product = _material.SequenceId + _material.MaterialCode;
            
            //_repo.RawMaterial.GetRawMaterialByMaterialNumber(materialNumber).Returns(_rawMaterial);
            //_repo.MaterialVendor.GetMaterialVendor(materialNumber).Returns(_material);

            _pln.CreateProductLotNumber(_material.MaterialNumber).Returns(product);

            //Act
            var productId = _pln.CreateProductLotNumber(materialNumber);


            //Assert
            Assert.Equal(product, productId);


        }
    }
}