using RavenAPI.DTO;
using RavenAPI.Models;


namespace RavenAPI.src;

public class RawMaterialSRC
{
    static RavenDBContext context = new RavenDBContext();

    public static List<RawMaterialDTO> getRawMaterial()
    {
        return context.RawMaterials
                .Join(context.VendorBatches, rm => rm.VendorBatchNumber, v => v.VendorBatchNumber, (RawMaterial, VendorBatch) => new RawMaterialDTO
                {
                    drumLotNumber = RawMaterial.DrumLotNumber,
                    materialNumber = RawMaterial.MaterialNumber,
                    batchNumber = RawMaterial.SapBatchNumber,
                    containerNumber = RawMaterial.ContainerNumber,
                    vendor = VendorBatch.VendorName,
                    vendorBatchNumber = VendorBatch.VendorBatchNumber,
                    inspectionLotNumber = RawMaterial.InspectionLotNumber,
                    sampleSubmitNumber = RawMaterial.SampleSubmitNumber,
                }).ToList();
    }
    static List<RawMaterialDTO> GetRawMaterial(int materialNumber)
    {
        return (from RawMaterial in context.RawMaterials
                join VendorBatch in context.VendorBatches on RawMaterial.VendorBatchNumber equals VendorBatch.VendorBatchNumber
                join MaterialNumber in context.MaterialNumbers on RawMaterial.MaterialNumber equals MaterialNumber.MaterialNumber1
                where MaterialNumber.ParentMaterialNumber == materialNumber
                select new RawMaterialDTO
                {
                    drumLotNumber = RawMaterial.DrumLotNumber,
                    materialNumber = RawMaterial.MaterialNumber,
                    batchNumber = RawMaterial.SapBatchNumber,
                    containerNumber = RawMaterial.ContainerNumber,
                    vendor = VendorBatch.VendorName,
                    vendorBatchNumber = VendorBatch.VendorBatchNumber,
                    inspectionLotNumber = RawMaterial.InspectionLotNumber,
                    sampleSubmitNumber = RawMaterial.SampleSubmitNumber,
                }).ToList();
    }
    public static List<string> getVendor(int parentMaterialNumber)
    {
        List<string> vendor = new List<string>();
        List<int> materialNumbers = MaterialDTO.getMaterialNumberFromParent(parentMaterialNumber);

        foreach (var number in materialNumbers)
        {
            string name = context.MaterialIds
                        .Where(v => v.MaterialNumber == number)
                        .Select(v => v.VendorName).First();
            vendor.Add(name);
        }
        return vendor;
    }
}