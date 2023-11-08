
using RavenDAL.DTO;
using RavenDAL.Models;

namespace RavenBAL.Tests
{
    public class RawMaterialTests
    {
        private readonly MaterialVendor _material = new MaterialVendor
        {
            MaterialNumber = 123456,
            MaterialCode = "AA",
            SequenceId = 100,
            TotalRecords = 100,
        };

        private readonly CreateRawMaterialDTO _createRawMaterialDTO = new CreateRawMaterialDTO
        {
            MaterialNumber = 123456,
            VendorLotNumber = "999-999-999",
            SampleId = "Raw45678",
        };


        private readonly IProductLotNumber _pln;
        private readonly IRepoWrapper _repo = Substitute.For<IRepoWrapper>();
        private readonly ILoggerManager _loggerManager = Substitute.For<ILoggerManager>();

        IEnumerable<RawMaterial> _rawMaterial = new List<RawMaterial>
        {
            new RawMaterial{ProductId = "100AA", MaterialNumber = 123456, VendorLotNumber = "999-999-999"},
            new RawMaterial{ProductId = "101AA", MaterialNumber = 123456, VendorLotNumber = "999-999-999"},
        };

        public RawMaterialTests()
        {
            _pln = new ProductLotNumber(_repo);   
        }

        [Fact]
        public void getNextProductLotNumber()
        {
            //Arrange
            _repo.RawMaterial.GetRawMaterialByMaterialNumber(_createRawMaterialDTO.MaterialNumber).Returns(_rawMaterial);

            ProductLotNumber lot = new ProductLotNumber(_repo);
            var id = "102AA";

            //Act
            var productId = lot.CreateProductLotNumber(_material);

            //Assert
            Assert.Equal(id, productId);
        }
        [Fact]
        public void getInitialProductLotNumber()
        {
            var product = _material.SequenceId + _material.MaterialCode;

            ProductLotNumber lot = new(_repo);
            var productId = lot.CreateProductLotNumber(_material);

            Assert.Equal(product, productId);
        }
        [Fact]
        public void UpdateProductLot()
        {

            var product = "100AA";
            var updateProduct = "100AA3L07";

            AlphabeticDate ad = new AlphabeticDate()
            {
                MonthNumber = 11,
                AlphabeticCode = "L",
            };

            DateTime today = new(2023-11-07);
            _repo.DateCode.GetDateCode(int.Parse(today.ToString("MM"))).Returns(ad);

            ProductLotNumber lot = new(_repo);
            var productId = lot.UpdateProductLotNumber(product);

            Assert.Equal(updateProduct, productId);
        }
        [Fact]
        public void CreateRawMaterial()
        {
            ProductLotNumber lot = new ProductLotNumber(_repo);
            

        }
    }
}