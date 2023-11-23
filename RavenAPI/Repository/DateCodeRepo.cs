using Contracts;
using RavenDB.Data;
using RavenDB.Models;

namespace Repository
{
    public class DateCodeRepo : RepoBase<AlphabeticDate>,IDateCode
    {
        public DateCodeRepo(RavenContext ctx) :base(ctx) { }
        public AlphabeticDate GetDateCode(int month)
        {
            return FindByCondition(x => x.MonthNumber.Equals(month)).FirstOrDefault();
        } 
    }
}
