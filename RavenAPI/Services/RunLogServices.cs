using RavenAPI.Data;
using RavenAPI.DTO;
using RavenAPI.src;

namespace RavenAPI.Services;

public class RunLogServices
{
    static RavenDBContext context = new RavenDBContext();

    static List<RunLogDTO> RunLog { get; } = MaterialRunLog.GetMaterialRunLog();

    public static List<RunLogDTO> GetAll() => RunLog;

    public static List<string> GetHourlyRead(int materialNumber) => MaterialRunLog.GetHourlyReads(materialNumber);

    public static List<string> GetPreStart(int materialNumber) => MaterialRunLog.GetPreStartCheck(materialNumber);

    public static List<List<String>> GetRunLog(int materialNumber) => MaterialRunLog.GetRunLog(materialNumber);
}