using RavenDAL.Models;
using RavenDAL.Data;

namespace RavenBAL.DTO;

public class SystemSetPointDTO
{
    public int? MaterialNumber { get; set; }
    public string? Nomenclature { get; set; }
    public bool? IsRequired { get; set; }
    public string? IndicatorType { get; set; }
    public string? Indicator { get; set; }
    public decimal? SetPoint { get; set; }
    public decimal? Variance { get; set; }

    public static List<SystemSetPointDTO> SetSystemSetPointS(int materialNumber)
    {
        RavenDBContext ctx = new RavenDBContext();

        return ctx.IndicatorSetPoints
            .Where(sp => sp.MaterialNumber == materialNumber)
            .Select(sp => new SystemSetPointDTO
            {
                MaterialNumber = sp.MaterialNumber,
                Nomenclature = sp.Nomenclature,
                Indicator = sp.Indicator,
                SetPoint = sp.SetPoint,
                Variance = sp.Variance,
                IndicatorType = sp.IndicatorType,
            }).ToList();
    }
    static List<string> FormatSystemSetPoints(List<SystemSetPointDTO> systemChecks)
    {
        List<string> output = new List<string>();

        foreach (var check in systemChecks)
        {
            var setPoint = string.Format("{0:0.##}", check.SetPoint);
            var variance = string.Format("{0:0.##}", check.Variance);


            if (check.Variance == -1)
            {
                output.Add(check.Nomenclature + " [" + check.Indicator + "] <" + setPoint);
            }
            else if (check.Indicator == null && check.SetPoint != null)
            {
                output.Add(check.Nomenclature + " < " + setPoint);
            }
            else if (check.Indicator != null && variance != null)
            {
                output.Add(check.Nomenclature + " [" + check.Indicator + "] " + setPoint + " (+/-) " + variance);
            }
            else if (check.Indicator != null)
            {
                output.Add(check.Nomenclature + " [" + check.Indicator + "]");
            }
            else
            {
                output.Add(check.Nomenclature);
            }
        }
        return output;
    }


    /*
    static List<SystemSetPointDTO> SystemSetPoint { get; } = ctx.IndicatorSetPoints
        .Select(sp => new SystemSetPointDTO
        {
            MaterialNumber = sp.MaterialNumber,
            Nomenclature = sp.Nomenclature,
            Indicator = sp.Indicator,
            SetPoint = sp.SetPoint,
            Variance = sp.Variance,
            IndicatorType = sp.IndicatorType,
        }).ToList();
    */
}