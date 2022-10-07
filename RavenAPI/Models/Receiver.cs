using System;
using System.Collections.Generic;

namespace RavenAPI.Models
{
    public partial class Receiver
    {
        public Receiver()
        {
            Productions = new HashSet<Production>();
        }

        public int ReceiverId { get; set; }
        public string ReceiverName { get; set; } = null!;

        public virtual ICollection<Production> Productions { get; set; }
    }
}
