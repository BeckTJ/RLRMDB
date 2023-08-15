using RavenBAL.src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenBAL.Interface
{
    public interface IRawMaterialDrum<T> where T : class
    {
        public string CreateRawMaterialDrum(int materialNumber,string vendorLot,string containerNumber,int weight,int batchNumber,long inspectionLotNumber);
        public T GetRawMaterialDrum(string productId);
        public IEnumerable<T> GetApprovedRawMaterial(int materialNumber);
        public IEnumerable<T> GetAllRawMaterialDrum(int materialNumber);
        void AddRawMaterialToProductLot(RawMaterialDrum rawMaterial);
        void AddRawMaterialToProductLot(MaterialVendor vendor);
    }
}
