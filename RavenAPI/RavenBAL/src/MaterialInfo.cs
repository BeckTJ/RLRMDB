using RavenDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenBAL.src
{
    public class MaterialInfo
    {
        public int MaterialNumber { get; set; }
        public int SequenceId { get; set; }
        public string MaterialCode { get; set; }
        public string UnitOfIssue { get; set; }
    }
}
