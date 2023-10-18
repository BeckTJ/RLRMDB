using System;
using System.Collections.Generic;

namespace RavenDAL.Models;

public partial class Receiver
{
    public string ReceiverName { get; set; } = null!;

    public virtual ICollection<Production> Productions { get; set; } = new List<Production>();

    public virtual ICollection<SystemReceiver> SystemReceivers { get; set; } = new List<SystemReceiver>();
}
