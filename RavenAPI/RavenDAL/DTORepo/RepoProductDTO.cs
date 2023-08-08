using RavenDAL.Data;
using RavenDAL.DTO;
using RavenDAL.Interface;
using RavenDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDAL.DTORepo
{
    public class RepoProductDTO : IProductDTO<ProductDTO>
    {
        private readonly IRepository<Production> _repo;

        public RepoProductDTO(IRepository<Production> repo)
        {
            _repo = repo;
        }
        public ProductDTO CreateProduct(ProductDTO _object)
        {
            var obj =  new Production
            {
                MaterialNumber = _object.MaterialNumber,
                ProductLotNumber = _object.ProductLotNumber,
                ProductBatchNumber = _object.BatchNumber,
                ReceiverName = _object.Reciever,
                ProcessOrder = _object.ProcessOrder,
            };
            _ = _repo.Create(obj);
            return _object;
        }

        public void Delete(ProductDTO _object)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductDTO> GetAllByMaterialNumber(int materialNumber)
        {
            throw new NotImplementedException();
        }

        public ProductDTO GetByProductId(string productId)
        {
            throw new NotImplementedException();
        }

        public void Update(ProductDTO _object)
        {
            throw new NotImplementedException();
        }
    }
}
