using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDAL.DTO
{
    public class MaterialDataDTO 
    {
        public decimal? SpecificGravity { get; set; } //Material
        public string? PermitNumber { get; set; } //Material
        public string? MaterialCode { get; set; } //MaterialId
        public string? UnitOfIssue { get; set; } //MaterialNumber
        public bool BatchManaged { get; set; } //MaterialNumber
        public int? SequenceId { get; set; } //MaterialId

    }
}
