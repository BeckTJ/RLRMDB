using RavenAPI.Models;
namespace RavenAPI.DTO;
public class MaterialDTO
{
    static RavenDBContext ctx = new RavenDBContext();

    public string? MaterialName { get; set; }
    public int? MaterialNumber { get; set; }
    public string? MaterialAbrev { get; set; }
    public string? MaterialCode { get; set; }
    public string? PermitNumber { get; set; }
    public string? UnitOfIssue { get; set; }
    public bool BatchManaged { get; set; }
    public int SequenceId { get; set; }
    public List<VendorDTO>? Vendors { get; set; }

    public MaterialDTO GetMaterial(int materialNumber)
    {

        return ctx.Materials
            .Where(x => x.MaterialNumber == materialNumber)
            .Select(x => new MaterialDTO
            {
                MaterialName = x.MaterialName,
                MaterialNumber = x.MaterialNumber,
                MaterialAbrev = x.MaterialNameAbreviation,
                PermitNumber = x.PermitNumber
            }).FirstOrDefault();
    }

    //Refactor Get list of Vendors via raw material.
    public static List<int> GetMaterialNumberFromParent(int materialNumber)
    {
        return ctx.MaterialNumbers
        .Where(x => x.ParentMaterialNumber == materialNumber)
        .Select(x => x.MaterialNumber1).ToList();
    }
    public static string getMaterialCode(int materialNumber)
    {
        return ctx.MaterialIds
            .Where(x => x.MaterialNumber == materialNumber)
            .Select(x => x.MaterialCode).FirstOrDefault();
    }
    public static int GetNumberOfRuns(int materialNumber)
    {

        return (int)ctx.Materials
            .Where(x => x.MaterialNumber == materialNumber)
            .Select(x => x.NumberOfRuns).FirstOrDefault();
    }

    public static int GetVendorMaterialNumber(int materialNumber, string vendor)
    {
        return (from MaterialId in ctx.MaterialIds
                join MaterialNumber in ctx.MaterialNumbers on MaterialId.MaterialNumber equals MaterialNumber.MaterialNumber1
                where MaterialNumber.ParentMaterialNumber == materialNumber && MaterialId.VendorName == vendor
                select MaterialId.MaterialNumber).FirstOrDefault();
    }
}