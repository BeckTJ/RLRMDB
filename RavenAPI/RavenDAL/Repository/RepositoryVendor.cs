
using RavenDAL.Interface;
using RavenDAL.Models;

namespace RavenDAL.Repository;

public class RepositoryVendor : IRepository<Vendor>
{
    public Task<Vendor> Create(Vendor _object)
    {
        throw new NotImplementedException();
    }

    public void Delete(Vendor _object)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Vendor> GetAll()
    {
        throw new NotImplementedException();
    }

    public Vendor GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(Vendor _object)
    {
        throw new NotImplementedException();
    }
}
