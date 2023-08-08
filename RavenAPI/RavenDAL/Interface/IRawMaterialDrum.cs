using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDAL.Interface
{
    public interface IRawMaterialDrum<T>
    {
        public Task<T> Create(T _object);
        public void Update(T _object);
        public void Delete(T _object);
        public IEnumerable<T> GetAllByMaterialNumber(int materialNumber);
        public IEnumerable<T> GetAllByVendorLotNumber(string vendorLotNumber);
        public T GetByProductId(string productId);

    }
}
