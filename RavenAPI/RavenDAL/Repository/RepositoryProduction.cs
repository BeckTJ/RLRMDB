using RavenDAL.Data;
using RavenDAL.Interface;
using RavenDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDAL.Repository
{
    public class RepositoryProduction : IRepository<Production>
    {
        private readonly RavenDBContext _ctx;
        public RepositoryProduction(RavenDBContext ctx) => _ctx = new RavenDBContext();
        public async Task<Production> Create(Production _object)
        {
            var obj = await _ctx.Productions.AddAsync(_object);
            _ctx.SaveChanges();
            return obj.Entity;
        }
        public void Delete(Production _object)
        {
            _ctx.Remove(_object);
            _ctx.SaveChanges();
        }

        public IEnumerable<Production> GetAll()
        {
            try
            {
                return _ctx.Productions.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public Production GetById(string id)
        {
            return _ctx.Productions.Where(x => x.ProductLotNumber == id).FirstOrDefault();
        }
        public void Update(Production _object)
        {
            _ctx.Productions.Update(_object);
            _ctx.SaveChanges();
        }
        public Production GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
