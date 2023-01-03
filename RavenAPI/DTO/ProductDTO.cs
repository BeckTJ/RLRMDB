namespace RavenAPI.DTO;

public class ProductDTO
{

    public int ProductId { get; set; }
    public string? ProductLotNumber { get; set; }
    public int MaterialNumber { get; set; }
    public int? ProductBatchNumber { get; set; }
    public decimal? ProcessOrder { get; set; }
    public int? ReceiverId { get; set; }
    public string? SampleSubmitNumber { get; set; }
    public DateTime? StartDate { get; set; }

}
