using RavenBAL.Interface;
using Moq;
using RavenDAL.Models;
using RavenDAL.DTO;
using RavenDAL.Interface;
using RavenBAL.Repository;

namespace RavenBALTests
{
    public class ProductIdTests 
    {
        private readonly MaterialDataDTO _materialDataDTO = new MaterialDataDTO
        {
            MaterialNumber = 123456,
            MaterialCode = "AA",
            SequenceId = 100,
        };
        private RawMaterialDTO _rawMaterialDrumDTO;
        private readonly Mock<IRepository<MaterialDataDTO>> _material;
        private readonly Mock<IRawMaterial<RawMaterialDTO>> _rawMaterial;
        private readonly ProductId _productId;

        public ProductIdTests()
        {
            _material = new();
            _rawMaterial = new();
            _material.Setup(m => m.GetById(It.IsAny<int>())).Returns(() => _materialDataDTO);
            _rawMaterial.Setup(m => m.GetAllByMaterialNumber(It.IsAny<int>())).Returns(() => _rawMaterial.Object
            .GetAllByMaterialNumber((int)_materialDataDTO.MaterialNumber));

            //_productId = new((IRepository<MaterialDataDTO>) _material,(IRawMaterialDrum<RawMaterialDrumDTO>)_rawMaterial);


            /* _productId.Setup(productId => productId.GetProductId(
                 It.IsAny<int>())).Returns((ProductId product) 
                 => new ProductId((IRepository<MaterialDataDTO>) _material,(IRawMaterialDrum<RawMaterialDrumDTO>)_rawMaterial)
                 .GetProductId((int)_materialDataDTO.MaterialNumber)); */
        }
        [Fact]
        public void FirstProductId()
        {
            int materialNumber = 123456;
            string productId = "100AA";
            _rawMaterialDrumDTO.ProductId = _productId.GetProductId(materialNumber);
            Assert.Equal(productId, _rawMaterialDrumDTO.ProductId);
        }

        [Fact]
        public void NextProductId()
        {
            string productId = "101AA";
            int materialNumber = 123456;
            _rawMaterialDrumDTO.ProductId = _productId.GetProductId(materialNumber);
            Assert.Equal(productId, _rawMaterialDrumDTO.ProductId);
        }
    }
}