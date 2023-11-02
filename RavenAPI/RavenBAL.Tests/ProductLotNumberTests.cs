
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

        public ProductLotNumberTests()
        {
            _pln = new ProductLotNumber();   
        }

        [Fact]
        public void getInitProductLotNumber()
        {
            //Arrange
            var materialNumber = 123456;


            //Act
            var productId = _pln.CreateProductLotNumber(materialNumber);


            //Assert


        }
    }
}