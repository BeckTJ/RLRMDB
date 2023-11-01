using Contracts;
using NSubstitute;
using RavenBAL.Interface;
using RavenBAL.Services;
using RavenDAL.DTO;
using Xunit;


namespace RavenBAL.Tests
{
    public class ProductLotNumberTests
    {
        private readonly IProductLotNumber _pln;
        private readonly IRepoWrapper _repo = Substitute.For<IRepoWrapper>();
        private readonly ILoggerManager _log = Substitute.For<ILoggerManager>();


        public ProductLotNumberTests()
        {
            _pln = new ProductLotNumber();
        }

        [Fact]
        public void GetProductLotNumber_FirstLotNumber()
        {
            //Arrange
            var material = 66666;
            var productId = "405SA";

            var id = _pln.CreateProductLotNumber(material).Returns(productId);


            //Act
            var lotNumber = _pln.CreateProductLotNumber(material);

            //Assert
            
        }
    }
}
