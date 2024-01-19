using RavenDB.Data;
using Microsoft.EntityFrameworkCore;

namespace Service.Tests.Fakes
{
    public class RavenContextFake : RavenContext
    {
        public RavenContextFake() : base(new DbContextOptionsBuilder<RavenContext>()
            .UseInMemoryDatabase(databaseName: $"Raven-{Guid.NewGuid()}").Options)
        { }

    }
}
