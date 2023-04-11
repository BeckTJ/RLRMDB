using RavenAPI.Models;

namespace RavenAPI.DTO;

public class RawMaterialDTO : MaterialDTO
{
    static RavenDBContext ctx = new RavenDBContext();
    public string? DrumLotNumber { get; set; }
    public int? DrumBatchNumber { get; set; }
    public string? ContainerNumber { get; set; }
    public VendorDTO Vendor { get; set; }
    public SampleDTO SampleSubmit { get; set; }
    public int? DrumWeight { get; set; }
    public RawMaterialDTO()
    {

    }
    public RawMaterialDTO(int materialNumber)
    {

    }
    public static List<string> GetRawMaterialByVendorBatch(string vendorBatchNumber)
    {
        return ctx.RawMaterials
            .Where(vb => vb.VendorBatchNumber == vendorBatchNumber)
            .Select(rm => rm.DrumLotNumber).ToList();
    }

    public static RawMaterialDTO GetRawMaterialByDrumNumber(string drumLotNumber)
    {
        return ctx.RawMaterials
        .Where(rm => rm.DrumLotNumber == drumLotNumber)
        .Join(ctx.VendorBatches, rm => rm.VendorBatchNumber, vb => vb.VendorBatchNumber, (rm, vb) => new RawMaterialDTO
        {
            DrumLotNumber = rm.DrumLotNumber,
            MaterialNumber = rm.MaterialNumber,
            Vendor = VendorDTO.GetVendor((int)rm.MaterialNumber, vb.VendorName),
            DrumBatchNumber = rm.SapBatchNumber,
            ContainerNumber = rm.ContainerNumber,
            SampleSubmit = SampleDTO.GetSample(rm.SampleSubmitNumber),
            DrumWeight = rm.DrumWeight,
        }).FirstOrDefault();
    }
    public static List<RawMaterialDTO> GetRawMaterial(int materialNumber, string vendor)
    {
        return (from RawMaterial in ctx.RawMaterials
                join VendorBatch in ctx.VendorBatches on RawMaterial.VendorBatchNumber equals VendorBatch.VendorBatchNumber
                where RawMaterial.MaterialNumber == materialNumber && VendorBatch.VendorName == vendor
                select new RawMaterialDTO
                {
                    DrumLotNumber = RawMaterial.DrumLotNumber,
                    MaterialNumber = RawMaterial.MaterialNumber,
                    DrumBatchNumber = RawMaterial.SapBatchNumber,
                    ContainerNumber = RawMaterial.ContainerNumber,
                    SampleSubmit = SampleDTO.GetSample(RawMaterial.SampleSubmitNumber),
                    DrumWeight = RawMaterial.DrumWeight,
                })
                /*.Where(x => x.SampleSubmit.Rejected == false)*/.ToList();
    }
    public static List<string> GetDrumLotNumbers(int materialNumber, string vendor)
    {
        return ctx.RawMaterials
            .Where(x => x.MaterialNumber == materialNumber)
            .Select(x => x.DrumLotNumber).ToList();
    }
}