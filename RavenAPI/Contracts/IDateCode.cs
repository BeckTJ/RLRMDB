using RavenDB.Models;

namespace Contracts
{
    public interface IDateCode
    {
        Task<AlphabeticDate> GetDateCode(int month);
    }
}
