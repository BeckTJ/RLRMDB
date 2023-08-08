using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenBAL.Interface
{
    public interface IVendorLot<T> where T : class
    {
        public T GetVendorLot(string LotNumber);
        public IEnumerable<T> GetAll(int materialNumber);
    }
}
