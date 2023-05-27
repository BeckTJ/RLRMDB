using RavenDAL.Models;
using RavenDAL.Data;

namespace RavenBAL.DTO;

public class RunDTO
{
    static RavenDBContext ctx = new RavenDBContext();
    public int? RunNumber { get; set; }
    public RawMaterialDTO RawMaterial { get; set; }
    public int? RawMaterialUsed { get; set; }
    public DateTime? StartDate { get; set; }
    public List<SystemStatusDTO> SystemStatus { get; set; }

    public RunDTO() { }
    public RunDTO(string lotNumber)
    {

    }

    public static List<RunDTO> GetRuns(string lotNumber)
    {
        return ctx.ProductRuns
            .Where(x => x.ProductLotNumber == lotNumber)
            .Select(x => new RunDTO
            {
                RunNumber = x.RunNumber,
                RawMaterial = RawMaterialDTO.GetRawMaterialByDrumNumber(x.DrumLotNumber),
                RawMaterialUsed = x.RawMaterialUsed,
                StartDate = x.RunStartDate,
            }).ToList();
    }
    public static RunDTO CurrentRun(string lotNumber)
    {
        return ctx.ProductRuns
            .Where(x => x.ProductLotNumber == lotNumber)
            .Select(x => new RunDTO
            {
                RunNumber = x.RunNumber,
                RawMaterial = RawMaterialDTO.GetRawMaterialByDrumNumber(x.DrumLotNumber),
                RawMaterialUsed = x.RawMaterialUsed,
                StartDate = x.RunStartDate
            }).OrderByDescending(x => x.RunNumber).FirstOrDefault();
    }
    public static int GetRunNumber(string lotnumber)
    {
        int runNumber = (int)ctx.ProductRuns
                    .Where(x => x.ProductLotNumber == lotnumber)
                    .Select(x => x.RunNumber).FirstOrDefault();
        if (runNumber != null)
        {
            return (int)runNumber;
        }
        return 1;
    }
}