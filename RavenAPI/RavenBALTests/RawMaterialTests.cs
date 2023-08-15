using RavenBAL.Interface;
using RavenBAL.src;

namespace RavenBAL.Tests
{
    public class RawMaterialTests
    {
        private readonly IRawMaterialDrum<RawMaterialDrum> _rawMaterial;
        public RawMaterialTests(IRawMaterialDrum<RawMaterialDrum> rawMaterial) 
        {
            _rawMaterial = rawMaterial;
        }
        [Fact]
        public void GetAllRawMateria()
        {
            throw new ArgumentNullException();
        }
    }
}
