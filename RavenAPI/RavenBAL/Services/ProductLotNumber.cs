using Contracts;
using RavenBAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenBAL.Services
{
    public class ProductLotNumber : IProductLotNumber
    {
        private IRepoWrapper _repo;

        public ProductLotNumber(IRepoWrapper repo) 
        {
            _repo = repo;
        }

        public string CreateProductLotNumber(int materialNumber)
        {
            var material = _repo.MaterialVendor.GetMaterialVendor(materialNumber);
            var rawMaterial = _repo.RawMaterial.GetRawMaterialByMaterialNumber(materialNumber).OrderByDescending(rm => rm.ProductId).FirstOrDefault();
            int id;
            if (rawMaterial != null)
            {
                if (rawMaterial.ProductId.Length == 10 || rawMaterial.ProductId.Length == 6)
                {
                    id = int.Parse(rawMaterial.ProductId[..3]) + 1;
                }
                else
                {
                    id = int.Parse(rawMaterial.ProductId[..2]) + 1;
                }
                return id + material.MaterialCode;
            }
            else
            { 
                var productId = material.SequenceId + material.MaterialCode;
                return productId;
            }
        }

        public string UpdateProductLotNumber(string lotNumber)
        {
            throw new NotImplementedException();
        }
    }
}
