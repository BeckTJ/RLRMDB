using RavenAPI.Models;

namespace RavenAPI.DTO;

public class SampleDTO
{
    static RavenDBContext ctx = new RavenDBContext();
    public string? SampleSubmitNumber { get; set; }
    public long? InspectionLotNumber { get; set; }
    public DateTime? SampleDate { get; set; }
    public bool? Rejected { get; set; }
    public DateTime? ReviewDate { get; set; }
    public DateTime? ExperationDate { get; set; }

    public static SampleDTO GetSample(string sampleSubmitNumber)
    {
        return ctx.SampleSubmits
            .Where(x => x.SampleSubmitNumber == sampleSubmitNumber)
            .Select(x => new SampleDTO
            {
                SampleSubmitNumber = x.SampleSubmitNumber,
                InspectionLotNumber = x.InspectionLotNumber,
                SampleDate = x.SampleDate,
                Rejected = x.Rejected,
                ReviewDate = x.ReviewDate,
                ExperationDate = x.ExperiationDate,
            }).FirstOrDefault();
    }
    public DateTime GetSampleDate(string sampleSubmitNumber)
    {
        return (DateTime)ctx.SampleSubmits
            .Where(x => x.SampleSubmitNumber == sampleSubmitNumber)
            .Select(x => x.SampleDate).FirstOrDefault();
    }
    public static bool SampleRequired(int materialNumber)
    {
        List<string> VLN = ctx.SampleRequireds
                    .Where(v => v.MaterialNumber == materialNumber && v.MaterialType == "Raw Material")
                    .Select(x => x.Vln).ToList();
        if (VLN.Count() > 1)
            return true;
        return false;
    }
    public static bool SampleRejected(string sampleNumber)
    {
        var isRejected = ctx.SampleSubmits
            .Where(x => x.SampleSubmitNumber == sampleNumber)
            .Select(x => x.Rejected).FirstOrDefault();

        if ((bool)isRejected)
            return true;
        return false;
    }
}

