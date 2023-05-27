using RavenDAL.Models;
using RavenDAL.Data;

namespace RavenBAL.DTO;
public class MaterialDTO
{
    static RavenDBContext ctx = new RavenDBContext();

    public string? MaterialName { get; set; } //Material
    public int? MaterialNumber { get; set; } //Material
    public string? MaterialAbrev { get; set; } //Material
    public decimal? SpecificGravity { get; set; } //Material
    public string? PermitNumber { get; set; } //Material
    public string? MaterialCode { get; set; } //MaterialId
    public string? UnitOfIssue { get; set; } //MaterialNumber
    public bool BatchManaged { get; set; } //MaterialNumber
    public int? SequenceId { get; set; } //MaterialId

    public MaterialDTO() { }
    public MaterialDTO(int materialNumber)
    {
        (from MaterialNumbers in ctx.MaterialNumbers
         join Material in ctx.Materials on MaterialNumbers.ParentMaterialNumber equals Material.MaterialNumber
         join MaterialId in ctx.MaterialIds on MaterialNumbers.MaterialNumber1 equals MaterialId.MaterialNumber
         where MaterialNumbers.MaterialNumber1 == materialNumber
         select new MaterialDTO
         {
             MaterialName = Material.MaterialName,
             MaterialNumber = MaterialNumbers.MaterialNumber1,
             MaterialAbrev = Material.MaterialNameAbreviation,
             SpecificGravity = Material.SpecificGravity,
             MaterialCode = MaterialId.MaterialCode,
             PermitNumber = Material.PermitNumber,
             UnitOfIssue = MaterialNumbers.UnitOfIssue,
             BatchManaged = MaterialNumbers.BatchManaged,
             SequenceId = MaterialId.SequenceId,
         }).FirstOrDefault();
    }
    public static int GetParentMaterialNumber(int materialNumber)
    {
        return ctx.MaterialNumbers
            .Where(x => x.MaterialNumber1 == materialNumber)
            .Select(x => x.ParentMaterialNumber).FirstOrDefault();
    }
    //Refactor Get list of Vendors via raw material.
    public static List<int> GetMaterialNumberFromParent(int materialNumber)
    {
        return ctx.MaterialNumbers
        .Where(x => x.ParentMaterialNumber == materialNumber)
        .Select(x => x.MaterialNumber1).ToList();
    }
    public static int GetSequenceId(int materialNumber)
    {
        return (int)ctx.MaterialIds
            .Where(x => x.MaterialNumber == materialNumber)
            .Select(x => x.SequenceId).FirstOrDefault();
    }
    public static string getMaterialCode(int materialNumber)
    {
        RavenDBContext ctx = new RavenDBContext();
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
    // write test to build this function
    public static int GetVendorMaterialNumber(int materialNumber, string vendor)
    {
        RavenDBContext ctx = new RavenDBContext();

        return (from MaterialId in ctx.MaterialIds
                join MaterialNumber in ctx.MaterialNumbers on MaterialId.MaterialNumber equals MaterialNumber.MaterialNumber1
                where MaterialNumber.ParentMaterialNumber == materialNumber && MaterialId.VendorName == vendor
                select MaterialId.MaterialNumber).FirstOrDefault();
    }
}