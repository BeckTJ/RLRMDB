using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenBAL.Interface
{
    public interface IMaterialVendor<T> where T : class
    {
        public T GetMaterialVendor(int materialNumber);
        public IEnumerable<T> GetAllMaterialVendor(int materialNumber);
    }
}
