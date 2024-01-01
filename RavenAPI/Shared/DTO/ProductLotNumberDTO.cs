using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public record ProductLotNumberDTO
    {
        public int MaterialNumber { get; set; }
        public int SequenceId { get; set; }
        public int TotalRecord { get; set; }
        public string? MaterialCode { get; set; }
    }
}
