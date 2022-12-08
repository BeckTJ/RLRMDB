using RavenAPI.Models;
using RavenAPI.Data;
using RavenAPI.DTO;

namespace RavenAPI.Services;

public class RunLogServices
{
    static RavenDBContext context = new RavenDBContext();

    static List<RunLogDTO> RunLog { get; } = context.IndicatorSetPoints
    .Select(i => new RunLogDTO
    {
        materialNumber = i.MaterialNumber,
        nomenclature = i.Nomenclature,
        indicator = i.Indicator,
        setPoint = i.SetPoint,
        variance = i.Variance,
    }).ToList();

    public static List<RunLogDTO> GetAll() => RunLog;

    public static List<RunLogDTO> Get(int materialNumber) => RunLog.Where(i => i.materialNumber == materialNumber).ToList();
}