using RavenDAL.DTO;
using RavenDAL.Models;
using RavenDAL.Data;
using RavenBAL.Interface;
using RavenDAL.Interface;

namespace RavenBAL.Repository;
public class RepoMaterial : IMaterial<Material>
{
    private readonly IRepository<MaterialDTO> _materialRepo;
    private readonly MaterialDTO _materialDTO;

    public RepoMaterial(IRepository<MaterialDTO> materialRepository)
    {
        _materialRepo = materialRepository;
        _materialDTO = new MaterialDTO();
    }

    public Task AddMaterial(Material material)
    {
        throw new NotImplementedException();
    }

    IEnumerable<Material> IMaterial<Material>.GetAll()
    {
        throw new NotImplementedException();
    }

    Material IMaterial<Material>.Get(int id)
    {
        throw new NotImplementedException();
    }

    public void Insert(Material entity)
    {
        throw new NotImplementedException();
    }

    public void Update(Material entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(Material entity)
    {
        throw new NotImplementedException();
    }

    public void Remove(Material entity)
    {
        throw new NotImplementedException();
    }
}