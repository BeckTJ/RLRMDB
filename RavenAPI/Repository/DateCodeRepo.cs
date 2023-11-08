using Contracts;
using RavenDAL.Data;
using RavenDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
