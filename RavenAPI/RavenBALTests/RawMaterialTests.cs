using RavenDAL.Interface;
using RavenDAL.DTO;
using RavenBAL.src;
using RavenBAL.Repository;
using RavenBAL.Interface;
using System.Web;
using RavenDAL.DTORepo;
using RavenDAL.Data;
using RavenDAL.Repository;
using RavenDAL.Models;
using NSubstitute;

namespace RavenBAL.Tests
{
    public class RawMaterialTests
    {
        private readonly MaterialDataDTO _materialDataDTO = new MaterialDataDTO
        {
            MaterialNumber = 123456,
            MaterialCode = "AA",
            SequenceId = 100,
        };


        [Fact]
        public void GetAllRawMateria()
        {
            var _ctx = Substitute.For<RavenDBContext>();
            
            _ctx.MaterialIds.Add(new MaterialId
            {
                MaterialNumber = (int)_materialDataDTO.MaterialNumber,
                MaterialCode = _materialDataDTO.MaterialCode,
                SequenceId = _materialDataDTO.SequenceId,
            });

            var _materialData = Substitute.For<IMaterialData<MaterialDataDTO>>();
            var material = _materialData.GetById((int)_materialDataDTO.MaterialNumber).Returns(_materialDataDTO);


            var productId = Substitute.For<IProductId>();

            var _rawMaterial = Substitute.For<IRawMaterial<RawMaterialDTO>>();
            RawMaterialDrum Material = new RawMaterialDrum();

            string id = _materialDataDTO.SequenceId + _materialDataDTO.MaterialCode;
            
            Material.ProductId = productId.GetProductId((int)_materialDataDTO.MaterialNumber);

            Assert.Contains("100A", Material.ProductId);
        }
    }
}
