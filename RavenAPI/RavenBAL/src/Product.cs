using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenBAL.src
{
    public class Product
    {
        public HighPurityMaterial Material { get; set; }
        public int ProductId { get; set; }
        public string Reciever { get; set; }
        public int RecieverLevel { get; set; }
        public string SampleId { get; set; }
        public ProcessOrder ProcessOrder { get; set; }
    }
}
