using RavenBAL.DTO;

namespace RavenBAL.src
{
    public class Product
    {
        public ProductDTO StartNewLot(int materialNumber)
        {
            ProductDTO product = new ProductDTO();
            product.MaterialNumber = materialNumber;
            product.ProductLotNumber = LotNumber.GetNextProductLotNumber(materialNumber);
            product.ProductRun[0].RunNumber = 1;
            return product;
        }
    }
}
