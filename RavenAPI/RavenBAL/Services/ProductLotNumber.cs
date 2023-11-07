using Contracts;
using RavenBAL.Interface;
using RavenDAL.Models;
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

        public ProductLotNumber()
        {
        }

        public ProductLotNumber(IRepoWrapper repo) 
        {
            _repo = repo;
        }

        public string CreateProductLotNumber(MaterialVendor material)
        {
            var rawMaterial = _repo.RawMaterial.GetRawMaterialByMaterialNumber(material.MaterialNumber).OrderByDescending(rm => rm.ProductId).FirstOrDefault();
            int id;
            if (rawMaterial != null)
            {
                if (rawMaterial.ProductId.Length == 10 || rawMaterial.ProductId.Length == 6)
                {
                    id = int.Parse(rawMaterial.ProductId[..4]) + 1;
                }
                else
                {
                    id = int.Parse(rawMaterial.ProductId[..3]) + 1;
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
            var todaysDate = DateTime.Now;
            var dateCode = _repo.DateCode.GetDateCode(int.Parse(todaysDate.ToString("MM")));
            var day = todaysDate.ToString("DD");
            return lotNumber + dateCode + day;
        }
    }
}
