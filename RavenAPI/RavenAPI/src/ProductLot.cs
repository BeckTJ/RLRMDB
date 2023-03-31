using RavenAPI.DTO;

namespace RavenAPI.src;

public class ProductLot
{
    public string? ProductLotNumber { get; set; }
    public List<string>? Receivers { get; set; }
    public List<string>? Vendors { get; set; }
    public List<string>? RawMaterial { get; set; }
    public void VerifyOpenLot(int materialNumber)
    {
        ProductDTO ProductLot = ProductDTO.GetCurrentProductLot(materialNumber);
        var totalRuns = MaterialDTO.GetNumberOfRuns(materialNumber);
        var currentRun = ProductLot.ProductRun.Last();
        var currentStatus = ProductLot.ProductRun.Last().SystemStatus.Last();

        if (SampleDTO.GetSample(ProductLot.SampleSubmit.SampleSubmitNumber) != null)
        {
            //Current run <= Max Run && Current Status != S
            if (currentRun.RunNumber <= totalRuns && currentStatus.SystemStatus != "S")
            {
                //open current lot and run
                OpenCurrentRun(materialNumber);
            }
            //Current Run <= Max Run && Current Status == S
            else if (currentRun.RunNumber <= totalRuns && currentStatus.SystemStatus == "S")
            {
                //start new run
                var newRun = currentRun.RunNumber + 1;
                //StartNewRun((int)newRun, ProductLot);
            }
            //Current Run = Max Run && Current Status == S
            else if (currentRun.RunNumber == totalRuns && currentStatus.SystemStatus == "S")
            {
                // sample/new lot
                StartNewLot(materialNumber);
            }
        }
    }
    public static ProductLot StartNewLot(int materialNumber)
    {
        ProductLot selection = new ProductLot();
        selection.ProductLotNumber = LotNumber.GetNextProductLotNumber(materialNumber);
        selection.Receivers = ProductDTO.GetReceivers(materialNumber);
        selection.Vendors = VendorDTO.getVendorFromParent(materialNumber);

        return selection;
    }
    public static ProductLot StartNewRun(int materialNumber, string vendor)
    {
        ProductLot selection = new ProductLot();
        selection.RawMaterial = RawMaterialSelection(materialNumber, vendor);

        return selection;
    }

    public ProductDTO OpenCurrentRun(int materialNumber)
    {
        return ProductDTO.GetCurrentProductLot(materialNumber);
    }

    /*Determine if RawMaterial Drum or Vendor batch will be
        used for user selection. Process needs to check if 
        sample has been approved, if drum has been used, 
    */
    public static List<string> RawMaterialSelection(int materialNumber, string vendor)
    {
        if (SampleDTO.SampleRequired(VendorDTO.GetVendor(materialNumber, vendor)))
        {
            return RawMaterialDTO.GetDrumLotNumbers(materialNumber, vendor);
        }
        return VendorDTO.GetVendorBatch(materialNumber, vendor);
    }
}