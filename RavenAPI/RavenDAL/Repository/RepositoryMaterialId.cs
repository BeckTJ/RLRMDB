
using RavenDAL.Interface;
using RavenDAL.Models;

namespace RavenDAL.Repository;

public class RepositoryMaterialId : IRepository<MaterialId>
{
    public Task<MaterialId> Create(MaterialId _object)
    {
        throw new NotImplementedException();
    }

    public void Delete(MaterialId _object)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<MaterialId> GetAll()
    {
        throw new NotImplementedException();
    }

    public MaterialId GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(MaterialId _object)
    {
        throw new NotImplementedException();
    }
}
