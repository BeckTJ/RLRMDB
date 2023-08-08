using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDAL.Interface
{
    public interface IVendor<T>
    {
        public Task<T> Create(T vendorLot);
        void Update(T vendorLot);
        void Delete(T vendorLot);

        IEnumerable<T> GetAllVendorBatch(int materialNumber);
        T GetVendorBatch(string vendorLotNumber);
    }
}
