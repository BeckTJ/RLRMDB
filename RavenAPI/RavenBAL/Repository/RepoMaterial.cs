using RavenDAL.DTO;
using RavenDAL.Models;
using RavenDAL.Data;
using RavenBAL.Interface;
using RavenDAL.Interface;
using RavenBAL.src;

namespace RavenBAL.Repository;
public class RepoMaterial : IMaterial<MaterialInfo>
{
    private readonly IMaterialData<MaterialDTO> _materialRepo;

    public RepoMaterial(IMaterialData<MaterialDTO> materialRepository)
    {
        _materialRepo = materialRepository;
    }

    public void Delete(MaterialInfo entity)
    {
        throw new NotImplementedException();
    }

    public MaterialInfo Get(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<MaterialInfo> GetAll()
    {
        throw new NotImplementedException();
    }

    public void Insert(MaterialInfo entity)
    {
        throw new NotImplementedException();
    }

    public void Remove(MaterialInfo entity)
    {
        throw new NotImplementedException();
    }

    public void Update(MaterialInfo entity)
    {
        throw new NotImplementedException();
    }
}