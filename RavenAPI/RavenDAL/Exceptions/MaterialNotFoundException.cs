
namespace RavenDB.Exceptions
{
    public sealed class MaterialNotFoundException : NotFoundException
    {
        public MaterialNotFoundException(int materialNumber)
            : base($"The material number { materialNumber} doesn't exist in the database.")
        {
        }
    }
}
