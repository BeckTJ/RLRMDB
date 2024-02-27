
namespace Service.Repo
{
    public class ProductLotNumber
    {
        public string CreateProductLotNumber(string materialCode, int sequenceId, string previousProductId)
        {
            if (previousProductId != null)
            {
                var id = GetNextSequenceId(previousProductId);
                return id + materialCode;
            }
            else
            {
                return sequenceId + materialCode;
            }
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
        public string UpdateProductLotNumber(string lotNumber, string dateCode)
        {
            var todaysDate = DateTime.Today;
            var year = todaysDate.Year % 10;
            var day = todaysDate.ToString("dd");

            return lotNumber + year + dateCode + day;
        }

    }
}
