
using Microsoft.Identity.Client;
using Shared.DTO;
using RavenDB.Models;
using Service;
using Xunit.Sdk;
using Service.Contracts;

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

        private readonly CreateRawMaterialDTO _createRawMaterialDTO = new()
        {
            MaterialNumber = 123456,
            VendorLotNumber = "999-999-999",
            SampleId = "Raw45678",
        };

        private readonly AlphabeticDate ad = new()
        {
            MonthNumber = 11,
            AlphabeticCode = "L",
        };
        IEnumerable<RawMaterial> _rawMaterial = new List<RawMaterial>
        {
            new RawMaterial{ProductId = "100AA", MaterialNumber = 123456, VendorLotNumber = "999-999-999"},
            new RawMaterial{ProductId = "101AA", MaterialNumber = 123456, VendorLotNumber = "999-999-999"},
        };
        IEnumerable<RawMaterial> _rawMaterialWithSample = new List<RawMaterial>
            {
                new RawMaterial{
                    MaterialNumber = 99999,
                    ProductId = "999AA9A99",
                    Sample = new SampleSubmit()
                    {
                        SampleSubmitNumber = "Raw11111",
                        SampleDate = new DateTime(2023 - 11 - 07),
                        Approved = true,
                        ExperiationDate =  DateTime.Today.AddYears(1),
                    },
                    VendorLotNumber = "999-999-999"
                    },
                new RawMaterial{
                    MaterialNumber = 99999,
                    ProductId = "998AA9A99",
                    Sample = new SampleSubmit()
                    {
                        SampleSubmitNumber = "Raw11111",
                        SampleDate = new DateTime(2023 - 11 - 07),
                        Approved = false,
                        ExperiationDate = null,
                    },
                    VendorLotNumber = "999-999-999"
                    },
                new RawMaterial{
                    MaterialNumber = 99999,
                    ProductId = "997AA9A99",
                    Sample = new SampleSubmit()
                    {
                        SampleSubmitNumber = "Raw11111",
                        SampleDate = new DateTime(2023 - 11 - 07),
                        Approved = true,
                        ExperiationDate = DateTime.Today.AddDays(-2),
                    },
                    VendorLotNumber = "999-999-999"
                    },
                new RawMaterial{
                    MaterialNumber = 99999,
                    ProductId = "997AA9A99",
                    Sample = new SampleSubmit()
                    {
                        SampleSubmitNumber = "Raw11111",
                        SampleDate = new DateTime(2023 - 11 - 07),
                        Rejected = true,
                        ExperiationDate = null,
                    },
                    VendorLotNumber = "999-999-999"
                    },

            };


        private readonly IRepoManager _repo = Substitute.For<IRepoManager>();
        private readonly IServiceManager _bal = Substitute.For<IServiceManager>();
        private readonly ILoggerManager _loggerManager = Substitute.For<ILoggerManager>();

        public RawMaterialTests()
        {
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
            var today = DateTime.Today.ToString("MM");
            var product = "100AA";
            var updateProduct = "100AA3L"+DateTime.Today.ToString("dd");

            _repo.DateCode.GetDateCode(int.Parse(today)).Returns(ad);

            ProductLotNumber lot = new(_repo);
            var productId = lot.UpdateProductLotNumber(product);

            Assert.Equal(updateProduct, productId);
        }
    }
}