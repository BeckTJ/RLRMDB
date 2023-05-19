using RavenAPI.Models;

namespace RavenAPI.DTO;

public class VendorDTO
{
    static RavenDBContext ctx = new RavenDBContext();

    public int? MaterialNumber { get; set; }
    public string? VendorName { get; set; }
    public string? VendorBatchNumber { get; set; }
    public SampleDTO SampleSubmit { get; set; }
    public int? Quantity { get; set; }

    internal static VendorDTO SetVendorBatch(int materialNumber, string vendor, string vendorBatch)
    {
        VendorDTO vb = new VendorDTO();
        vb.MaterialNumber = materialNumber;
        vb.VendorName = vendor;
        vb.VendorBatchNumber = vendorBatch;
        vb.SampleSubmit = SampleDTO.SetSample();

        return vb;
    }

    internal static VendorDTO GetVendor(int materialNumber, string? vendorName)
    {
        return ctx.VendorBatches
            .Where(v => v.VendorName == vendorName && v.MaterialNumber == materialNumber)
            .Select(x => new VendorDTO
            {
                MaterialNumber = x.MaterialNumber,
                VendorName = x.VendorName,
                VendorBatchNumber = x.VendorBatchNumber,
                SampleSubmit = SampleDTO.GetSample(x.SampleSubmitNumber),
                Quantity = x.Quantity
            }).FirstOrDefault();
    }
    internal static VendorDTO GetVendor(string batchNumber)
    {
        return ctx.VendorBatches
            .Where(v => v.VendorBatchNumber == batchNumber)
            .Select(x => new VendorDTO
            {
                MaterialNumber = x.MaterialNumber,
                VendorName = x.VendorName,
                VendorBatchNumber = x.VendorBatchNumber,
                SampleSubmit = SampleDTO.GetSample(x.SampleSubmitNumber),
                Quantity = x.Quantity
            }).FirstOrDefault();
    }


    public static List<string> GetVendorBatchList(int materialNumber, string vendor)
    {
        return ctx.VendorBatches
        .Where(vb => vb.VendorName == vendor && vb.MaterialNumber == materialNumber)
        .Select(vb => vb.VendorBatchNumber).ToList();
    }

    public static List<string> getVendorFromParent(int parentMaterialNumber)
    {
        RavenDBContext ctx = new RavenDBContext();

        List<string> vendor = new List<string>();
        List<int> materialNumbers = MaterialDTO.GetMaterialNumberFromParent(parentMaterialNumber);

        foreach (var number in materialNumbers)
        {
            vendor.Add(ctx.MaterialIds
                        .Where(v => v.MaterialNumber == number)
                        .Select(v => v.VendorName).FirstOrDefault());
        }
        return vendor;
    }
}