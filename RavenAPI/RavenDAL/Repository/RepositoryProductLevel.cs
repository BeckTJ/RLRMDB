
using RavenDAL.Interface;
using RavenDAL.Models;

namespace RavenDAL.Repository;

public class RepositoryProductLevel : IRepository<ProductLevel>
{
    public Task<ProductLevel> Create(ProductLevel _object)
    {
        throw new NotImplementedException();
    }

    public void Delete(ProductLevel _object)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ProductLevel> GetAll()
    {
        throw new NotImplementedException();
    }

    public ProductLevel GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(ProductLevel _object)
    {
        throw new NotImplementedException();
    }
}
