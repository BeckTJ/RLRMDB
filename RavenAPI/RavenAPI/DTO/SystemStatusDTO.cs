using RavenAPI.Models;

namespace RavenAPI.DTO;

public class SystemStatusDTO
{
    static RavenDBContext ctx = new RavenDBContext();
    public string? SystemStatus { get; set; }
    public bool? VisualVerification { get; set; }
    public int? ReboilerLevel { get; set; }
    public int? PrefractionLevel { get; set; }
    public int? ReceiverLevel { get; set; }
    public List<RunLogDTO> RunLogs { get; set; }
    public TimeSpan? ReadTime { get; set; }


    public static List<SystemStatusDTO> GetRunLevels(string? lotNumber)
    {
        return ctx.ProductLevels
            .Where(x => x.ProductLotNumber == lotNumber)
            .Select(x => new SystemStatusDTO
            {
                SystemStatus = x.SystemStatus,
                VisualVerification = x.VisualVerification,
                ReboilerLevel = x.ReboilerLevel,
                PrefractionLevel = x.PrefractionLevel,
                ReceiverLevel = x.ReceiverLevel,
                ReadTime = x.ReadTime
            }).ToList();
    }
    public static List<SystemStatusDTO> GetCurrentRun(int run, string lotNumber)
    {
        return ctx.ProductLevels
            .Where(x => x.ProductLotNumber == lotNumber && x.RunNumber == run)
            .Select(x => new SystemStatusDTO
            {
                SystemStatus = x.SystemStatus,
                VisualVerification = x.VisualVerification,
                ReboilerLevel = x.ReboilerLevel,
                PrefractionLevel = x.PrefractionLevel,
                ReceiverLevel = x.ReceiverLevel,
                ReadTime = x.ReadTime
            }).ToList();
    }
    public static SystemStatusDTO GetCurrentStatus(int run, string lotNumber)
    {
        return GetCurrentRun(run, lotNumber).OrderByDescending(x => x.ReadTime).First();
    }
}