
using RavenBAL.Interface;
using Repository;

namespace RavenBAL.Services
{
    public class BalWrapper: IBalWrapper
    {
        private RepoWrapper _repo;
        private IRawMaterialService _rawMaterialService;
        private IProductLotNumber _productLotNumber;

        public BalWrapper(RepoWrapper repo)
        {
            _repo = repo;
        }
        public IRawMaterialService RawMaterialService { 
            get 
            { 
                _rawMaterialService ??= new RawMaterialServices(_repo);
                return _rawMaterialService; 
            }
        }
        public IProductLotNumber ProductLotNumber
        {
            get
            {
                _productLotNumber ??= new ProductLotNumber(_repo);
                return _productLotNumber;
            }
        }
    }
}
