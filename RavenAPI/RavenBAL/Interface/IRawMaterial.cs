using RavenDAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenBAL.Interface
{
    public interface IRawMaterial<T> where T : class
    {
        public IEnumerable<T> GetRawMaterial(int materialNumber);
        public IEnumerable<T> GetAllRawMaterial(int parentMaterialNumber);

        void AddRawMaterialToProductLot(RawMaterialDrumDTO DrumId);
        void AddRawMaterialToProductLot(VendorBatchDTO batchId);
    }
}
