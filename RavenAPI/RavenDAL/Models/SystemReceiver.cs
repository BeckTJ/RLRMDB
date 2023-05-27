using System;
using System.Collections.Generic;

namespace RavenDAL.Models
{
    public partial class SystemReceiver
    {
        public int ReceiverId { get; set; }
        public int? MaterialNumber { get; set; }
        public string? ReceiverName { get; set; }
        public int? MaxReceiverLevel { get; set; }

        public virtual Material? MaterialNumberNavigation { get; set; }
        public virtual Receiver? ReceiverNameNavigation { get; set; }
    }
}
