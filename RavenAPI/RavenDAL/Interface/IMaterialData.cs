using RavenDAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDAL.Interface
{
    public interface IMaterialData<T> where T : class
    {
        public T GetById(int materialNumber);
        public IEnumerable<T> GetMaterialNumberFromParent(int parentMaterialNumber);
        public IEnumerable<T> GetAll();

    }
}
