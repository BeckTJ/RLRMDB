using Contracts;
using RavenBAL.Interface;
using RavenDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
            var todaysDate = DateTime.Today;
            var today = todaysDate.ToString("MM");
            var dateCode = _repo.DateCode.GetDateCode(int.Parse(today)).AlphabeticCode;
            var year = todaysDate.Year % 10;
            var day = todaysDate.ToString("dd");
            return lotNumber + year + dateCode + day;
        }
    }
}
