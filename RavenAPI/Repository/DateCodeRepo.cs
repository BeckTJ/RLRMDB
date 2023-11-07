using Contracts;
using RavenDAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class DateCodeRepo : IDateCode
    {
        private RavenContext _ctx;
        public DateCodeRepo(RavenContext ctx) 
        {
            _ctx = ctx;
        }
        public string GetDateCode(int month)
        {
            return _ctx.AlphabeticDates.FirstOrDefault(x => x.MonthNumber == month).AlphabeticCode;
        }
    }
}
