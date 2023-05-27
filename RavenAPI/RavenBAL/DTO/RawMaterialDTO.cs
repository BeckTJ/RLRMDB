using RavenDAL.Models;
using RavenDAL.Data;

using RavenBAL.src;

namespace RavenBAL.DTO;

public class RawMaterialDTO : MaterialDTO
{
    static RavenDBContext ctx = new RavenDBContext();
    public string? DrumLotNumber { get; set; }
    public int? DrumBatchNumber { get; set; }
    public string? ContainerNumber { get; set; }
    public VendorDTO Vendor { get; set; }
    public int? DrumWeight { get; set; }

    public static void SetRawMaterial(int materialNumber, int batchNumber, string ctn, string vendor, string vendorBatch)
    {
        RawMaterialDTO rm = new RawMaterialDTO();
        rm.DrumLotNumber = LotNumber.GetNextProductLotNumber(materialNumber);
        rm.DrumBatchNumber = batchNumber;
        rm.ContainerNumber = ctn;
        rm.Vendor = VendorDTO.SetVendorBatch(materialNumber, vendor, vendorBatch);
    }
    public static RawMaterialDTO SetRawMaterialFromVendorBatch(string rawMaterial)
    {
        RawMaterialDTO rm = new RawMaterialDTO();
        rm.Vendor = VendorDTO.GetVendor(rawMaterial);
        rm.DrumLotNumber = LotNumber.GetNextProductLotNumber((int)rm.Vendor.MaterialNumber);
        rm.DrumBatchNumber = null; // Fix DB to have vendorbatch set up with material batch number
        rm.ContainerNumber = null;
        rm.DrumWeight = null; // set standard Drum Weight in DB for each material
        return rm;
    }
    public static RawMaterialDTO GetRawMaterialByDrumNumber(string drumLotNumber)
    {
        return ctx.RawMaterials
        .Where(rm => rm.DrumLotNumber == drumLotNumber)
        .Join(ctx.VendorBatches, rm => rm.VendorBatchNumber, vb => vb.VendorBatchNumber, (rm, vb) => new RawMaterialDTO
        {
            DrumLotNumber = rm.DrumLotNumber,
            MaterialNumber = rm.MaterialNumber,
            Vendor = VendorDTO.GetVendor(rm.VendorBatchNumber),
            DrumBatchNumber = rm.SapBatchNumber,
            ContainerNumber = rm.ContainerNumber,
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
                    Vendor = VendorDTO.GetVendor(RawMaterial.VendorBatchNumber),
                    ContainerNumber = RawMaterial.ContainerNumber,
                    DrumWeight = RawMaterial.DrumWeight,
                }).ToList();
    }
    public static List<string> GetDrumLotNumbersList(int materialNumber, string vendor)
    {
        return ctx.RawMaterials
            .Where(x => x.MaterialNumber == materialNumber)
            .Select(x => x.DrumLotNumber).ToList();
    }
}