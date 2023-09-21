using RavenDAL.DTO;
using RavenDAL.Data;
using RavenDAL.DTORepo;
using RavenBAL.Interface;
using RavenBAL.src;
using RavenDAL.Interface;

namespace RavenBAL.Repository;

public class RepoRawMaterialDrum : IRawMaterialDrum<RawMaterialDrum>
{
    private readonly IRawMaterial<RawMaterialDTO> _rawMaterial;
    private readonly IProductId _productId;
    private readonly ISample<Sample> _sample;

    public RepoRawMaterialDrum(IRawMaterial<RawMaterialDTO> rawMaterial,ISample<Sample> sample, IProductId productId)
    {
        _rawMaterial = rawMaterial;
        _sample = sample;
        _productId = productId;
    }

    public void AddRawMaterialToProductLot(RawMaterialDrum rawMaterial)
    {
        throw new NotImplementedException();
    }

    public void AddRawMaterialToProductLot(MaterialVendor vendor)
    {
        throw new NotImplementedException();
    }
    public RawMaterialDrum GetRawMaterialDrum(string productId)
    {
        var material = _rawMaterial.GetByProductId(productId);
        return new RawMaterialDrum
        {
            MaterialNumber = material.MaterialNumber,
            ProductId = material.ProductId,
            VendorLotNumber = material.VendorLotNumber,
            ContainerNumber = material.ContainerNumber,
            Weight = (int)material.DrumWeight,
            BatchNumber = material.BatchNumber
        };
    }
    public IEnumerable<RawMaterialDrum> GetAllRawMaterialDrum(int materialNumber)
    {
        var material = _rawMaterial.GetAllByMaterialNumber(materialNumber);
        List<RawMaterialDrum> rawMaterial = new List<RawMaterialDrum>();
        foreach (var item in material)
        {
            rawMaterial.Add(new RawMaterialDrum
            {
                MaterialNumber = item.MaterialNumber,
                ProductId = item.ProductId,
                VendorLotNumber = item.VendorLotNumber,
                ContainerNumber = item.ContainerNumber,
                Weight = (int)item.DrumWeight,
                BatchNumber = item.BatchNumber,
                SampleId = item.SampleId
            }); 
        }
        return rawMaterial;
    }
    public IEnumerable<RawMaterialDrum> GetApprovedRawMaterial(int materialNumber)
    {
        string ApprovedSample = "";
        var material = _rawMaterial.GetAllByMaterialNumber(materialNumber).OrderBy(x => x.SampleId);
        List<RawMaterialDrum> rawMaterial = new();

        foreach (var item in material)
        {
            if (item.SampleId == ApprovedSample || _sample.SampleApproved(item.SampleId))
            {
                if (item.SampleId != ApprovedSample)
                {
                    ApprovedSample = item.SampleId;
                }

                rawMaterial.Add(new RawMaterialDrum
                {
                    MaterialNumber = item.MaterialNumber,
                    ProductId = item.ProductId,
                    VendorLotNumber = item.VendorLotNumber,
                    ContainerNumber = item.ContainerNumber,
                    Weight = (int)item.DrumWeight,
                    BatchNumber = item.BatchNumber,
                    SampleId = item.SampleId
                });
            }
        }
        return rawMaterial;
    }

    public string CreateRawMaterialDrum(int materialNumber, string vendorLot, string containerNumber, int weight, int batchNumber, long inspectionLotNumber)
    {
        var productId = _productId.GetProductId(materialNumber);

        var rawMaterial = _rawMaterial.Create(new RawMaterialDTO
        {
            MaterialNumber = materialNumber,
            ProductId = productId,
            VendorLotNumber = vendorLot,
            ContainerNumber = containerNumber,
            DrumWeight = weight,
            BatchNumber = batchNumber,
            InspectionLotNumber = inspectionLotNumber
        });
        return productId;
    }
}