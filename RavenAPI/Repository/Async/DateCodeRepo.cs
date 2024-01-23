using Contracts;
using Microsoft.EntityFrameworkCore;
using RavenDB.Data;
using RavenDB.Models;

namespace Repository.Async
{
    internal sealed class DateCodeRepo : RepoBase<AlphabeticDate>, IDateCode
    {
        public DateCodeRepo(RavenContext ctx) : base(ctx) { }
        public async Task<AlphabeticDate> GetDateCode(int month) =>
            await FindByCondition(x => x.MonthNumber.Equals(month)).FirstOrDefaultAsync();
    }
}
