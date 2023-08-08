
using RavenDAL.Interface;
using RavenDAL.Models;

namespace RavenDAL.Repository;

public class RepositoryMaterialNumber : IRepository<MaterialNumber>
{
    public Task<MaterialNumber> Create(MaterialNumber _object)
    {
        throw new NotImplementedException();
    }

    public void Delete(MaterialNumber _object)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<MaterialNumber> GetAll()
    {
        throw new NotImplementedException();
    }

    public MaterialNumber GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(MaterialNumber _object)
    {
        throw new NotImplementedException();
    }
}
