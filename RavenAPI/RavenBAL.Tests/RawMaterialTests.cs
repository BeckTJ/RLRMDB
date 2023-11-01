using System;
using System.Linq;
using Xunit;
using RavenBAL.Interface;
using RavenBAL.Services;

namespace RavenBAL.Tests
{
    public class RawMaterialTests
    {
        private readonly IRawMaterialService _rms;

        public RawMaterialTests()
        {
            _rms = new RawMaterialService()
        }

        [Fact]
        public void CreateRawMaterialIdTest()
        {

        }
    }
}
