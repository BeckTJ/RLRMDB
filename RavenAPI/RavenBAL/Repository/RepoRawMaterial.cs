using RavenDAL.DTO;
using RavenDAL.Data;
using RavenDAL.DTORepo;
using RavenBAL.Interface;
using RavenBAL.src;
using RavenDAL.Interface;

namespace RavenBAL.Repository;

public class RepoRawMaterial : IRawMaterial<RawMaterialData>
{
    private readonly IVendorLot<VendorLot> _rawMaterial;
    private readonly IMaterialData<MaterialDataDTO> _material;

    public RepoRawMaterial(IVendorLot<VendorLot> rawMaterial, IMaterialData<MaterialDataDTO> material)
    {
        _rawMaterial = rawMaterial;
        _material = material;
    }
    public void SetRawMaterialDrum(string materialNumber)
    {
        
    }

    public void AddRawMaterialToProductLot(RawMaterialDrumDTO DrumId)
    {
        throw new NotImplementedException();
    }

    public void AddRawMaterialToProductLot(VendorBatchDTO batchId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<RawMaterialData> GetAllRawMaterial(int parentMaterialNumber)
    {
        //Repeating the same material number???

        List<RawMaterialData> rm = new List<RawMaterialData>();
        RawMaterialData temp = new RawMaterialData();

        var materialNumbers = _material.GetMaterialNumberFromParent(parentMaterialNumber);

        foreach(var material in materialNumbers)
        {
            temp.MaterialData = material;
            temp.VendorLot = _rawMaterial.GetAll((int)material.MaterialNumber).ToList();
            rm.Add(temp);
        }
        return rm;
    }
    public IEnumerable<RawMaterialData> GetRawMaterial(int materialNumber)
    {
        throw new NotImplementedException();
    }
}