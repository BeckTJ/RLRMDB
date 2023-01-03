using RavenAPI.DTO;
using RavenAPI.Data;

namespace RavenAPI.src;

public class MaterialRunLog
{
    static RavenDBContext context = new RavenDBContext();

    public static List<List<string>> GetRunLog(int materialNumber)
    {
        List<List<string>> runLog = new List<List<string>>();
        runLog.Add(GetPreStartCheck(materialNumber));
        runLog.Add(GetHourlyReads(materialNumber));
        return runLog;
    }

    public static List<string> GetHourlyReads(int materialNumber) =>
        FormatRunLog(SetMaterialRunLog(materialNumber)
        .Where(x => x.indicatorType != "Pre Start")
        .ToList());
    public static List<string> GetPreStartCheck(int materialNumber) =>
        FormatRunLog(SetMaterialRunLog(materialNumber)
        .Where(x => x.indicatorType == "Pre Start")
        .ToList());

    public static List<RunLogDTO> GetMaterialRunLog()
    {
        return context.IndicatorSetPoints
            .Select(i => new RunLogDTO
            {
                materialNumber = i.MaterialNumber,
                nomenclature = i.Nomenclature,
                indicator = i.Indicator,
                setPoint = i.SetPoint,
                variance = i.Variance,
                indicatorType = i.IndicatorType,
            }).ToList();
    }

    static List<RunLogDTO> SetMaterialRunLog(int materialNumber)
    {
        return context.IndicatorSetPoints
            .Select(i => new RunLogDTO
            {
                materialNumber = i.MaterialNumber,
                nomenclature = i.Nomenclature,
                indicator = i.Indicator,
                setPoint = i.SetPoint,
                variance = i.Variance,
                indicatorType = i.IndicatorType,
                isRequired = i.IsRequired,
            })
            .Where(x => x.materialNumber == materialNumber)
            .OrderBy(x => x.indicatorType)
            .ToList();
    }
    static List<string> FormatRunLog(List<RunLogDTO> systemChecks)
    {
        List<string> output = new List<string>();

        foreach (var check in systemChecks)
        {
            var setPoint = string.Format("{0:0.##}", check.setPoint);
            var variance = string.Format("{0:0.##}", check.variance);


            if (check.variance == -1)
            {
                output.Add(check.nomenclature + " [" + check.indicator + "] <" + setPoint);
            }
            else if (check.indicator == null && check.setPoint != null)
            {
                output.Add(check.nomenclature + " < " + setPoint);
            }
            else if (check.indicator != null && variance != null)
            {
                output.Add(check.nomenclature + " [" + check.indicator + "] " + setPoint + " (+/-) " + variance);
            }
            else if (check.indicator != null)
            {
                output.Add(check.nomenclature + " [" + check.indicator + "]");
            }
            else
            {
                output.Add(check.nomenclature);
            }
        }
        return output;
    }
}