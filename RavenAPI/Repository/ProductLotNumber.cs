using Contracts;
using RavenDB.Data;
using RavenDB.Models;
using Service.Contracts;

namespace Repository
{
    public class ProductLotNumber : IProductLotNumber<MaterialVendor>
    {
        private readonly RavenContext _ctx;

        public ProductLotNumber(RavenContext ctx)
        {
            _ctx = ctx;
        }

        public string CreateProductLotNumber(int materialNumber)
        {
            string productId = null;

            var material = _ctx.MaterialVendors.Where(m => m.MaterialNumber == materialNumber).FirstOrDefault();
            
            var lastProductId = _ctx.RawMaterials.Where(m => m.MaterialNumber == materialNumber)
                    .OrderByDescending(x => x.DrumLotNumber).FirstOrDefault().DrumLotNumber;
            
            if (lastProductId != null)
            {
                var id = GetNextSequenceId(lastProductId);
                productId = id + material.MaterialCode;
            }
            else
            {
                productId = material.SequenceId + material.MaterialCode;
            }
            return UpdateProductLotNumber(productId);
        }

        private int GetNextSequenceId(string productId)
        {
            if (productId.Length == 10 || productId.Length == 6)
            {
                return int.Parse(productId[..4]) + 1;
            }
            else
            {
                return int.Parse(productId[..3]) + 1;
            }
        }

        public string UpdateProductLotNumber(string lotNumber)
        {
            var todaysDate = DateTime.Today;
            var today = todaysDate.ToString("MM");
            var dateCode = _ctx.AlphabeticDates.FirstOrDefault(x => x.MonthNumber == int.Parse(today)).AlphabeticCode;
            var year = todaysDate.Year % 10;
            var day = todaysDate.ToString("dd");

            return lotNumber + year + dateCode + day;
        }
    }
}
