
namespace RavenDB.Exceptions
{
    public sealed class SampleDataNotFoundException : NotFoundException
    {
        public SampleDataNotFoundException(int materialNumber) 
            : base($"No Sample Data Found for: {materialNumber}")
        { }
    }
}
