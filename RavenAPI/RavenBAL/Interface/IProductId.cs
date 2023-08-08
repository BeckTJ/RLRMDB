using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenBAL.Interface
{
    public interface IProductId
    {
        string GetProductId(int materialNumber);
    }
}
