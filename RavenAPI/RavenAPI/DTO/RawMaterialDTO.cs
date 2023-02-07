namespace RavenAPI.DTO;

public class RawMaterialDTO
{
    public string? drumLotNumber { get; set; }
    public int? materialNumber { get; set; }
    public int? batchNumber { get; set; }
    public string? containerNumber { get; set; }
    public string? vendor { get; set; }
    public string? vendorBatchNumber { get; set; }
    public long? inspectionLotNumber { get; set; }
    public string? sampleSubmitNumber { get; set; }
}