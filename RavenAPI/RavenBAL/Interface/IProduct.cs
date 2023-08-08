using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenBAL.Interface
{
    public interface IProduct<T> where T : class
    {
        IEnumerable<T> GetAllProduct(int MaterialNumber);
        T GetProduct(T product);
        void UpdateProduct(T product);
        void DeleteProduct(T product);

    }
}
