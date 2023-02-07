namespace RavenAPI.DTO;

public class RunLogDTO
{
    public int materialNumber { get; set; }
    public string nomenclature { get; set; } = null!;
    public bool? isRequired { get; set; }
    public string? indicatorType { get; set; }
    public string? indicator { get; set; }
    public decimal? setPoint { get; set; }
    public decimal? variance { get; set; }
}