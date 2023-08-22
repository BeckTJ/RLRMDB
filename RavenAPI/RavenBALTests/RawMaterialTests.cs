using Moq;
using RavenDAL.Interface;
using RavenDAL.DTO;
using RavenBAL.src;
using RavenBAL.Repository;

namespace RavenBAL.Tests
{
    public class RawMaterialTests
    {
        private readonly Mock<IRawMaterial<RawMaterialDTO>> _rawMaterial = new Mock<IRawMaterial<RawMaterialDTO>>();
        private readonly Mock<IMaterialData<MaterialDataDTO>> _materialData = new Mock<IMaterialData<MaterialDataDTO>>(); 
        private readonly MaterialDataDTO _materialDataDTO = new MaterialDataDTO
        {
            MaterialNumber = 123456,
            MaterialCode = "AA",
            SequenceId = 100,
        };

        RawMaterialDrum Material = new RawMaterialDrum();

        [Fact]
        public void GetAllRawMateria()
        {
            
        }
    }
}
