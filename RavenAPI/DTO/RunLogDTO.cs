namespace RavenAPI.DTO;

public class RunLogDTO
{
    public int materialNumber { get; set; }
    public string nomenclature { get; set; } = null!;
    public string? indicator { get; set; }
    public decimal? setPoint { get; set; }
    public decimal? variance { get; set; }
}