using RavenDB.Models;

namespace Contracts
{
    public interface IDateCode
    {
        AlphabeticDate GetDateCode(int month);
    }
}
