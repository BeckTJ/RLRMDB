using System;
using System.Collections.Generic;

namespace RavenDAL.Models
{
    public partial class Receiver
    {
        public Receiver()
        {
            Productions = new HashSet<Production>();
            SystemReceivers = new HashSet<SystemReceiver>();
        }

        public string ReceiverName { get; set; } = null!;

        public virtual ICollection<Production> Productions { get; set; }
        public virtual ICollection<SystemReceiver> SystemReceivers { get; set; }
    }
}
