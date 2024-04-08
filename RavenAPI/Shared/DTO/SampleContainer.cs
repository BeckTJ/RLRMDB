using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public record SampleContainer
    {
        public string? ContainerType { get; set; }
        public int Quantity { get; set; }
        public int SampleSize { get; set; }
        public string? UnitOfIssue { get; set; }
    }
}
