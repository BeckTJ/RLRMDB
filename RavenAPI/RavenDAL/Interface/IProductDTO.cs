using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDAL.Interface
{
    public interface IProductDTO<T> 
    {
        public T CreateProduct(T _object);
        public void Update(T _object);
        public void Delete(T _object);
        public IEnumerable<T> GetAllByMaterialNumber(int materialNumber);
        public T GetByProductId(string productId);

    }
}
