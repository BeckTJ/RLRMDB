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
    public class RepositoryRawMaterial : IRepository<RawMaterial>
    {
        RavenDBContext _ctx;
        public RepositoryRawMaterial(RavenDBContext ctx) => _ctx = new RavenDBContext();
        public async Task<RawMaterial> Create(RawMaterial _object)
        {
            var obj = await _ctx.RawMaterials.AddAsync(_object);
            _ctx.SaveChanges();
            return obj.Entity;
        }
        public void Delete(RawMaterial _object)
        {
            _ctx.Remove(_object);
            _ctx.SaveChanges();
        }

        public IEnumerable<RawMaterial> GetAll()
        {
            try
            {
                return _ctx.RawMaterials.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public RawMaterial GetById(int id)
        {
            return _ctx.RawMaterials.Where(x => x.MaterialNumber == id).FirstOrDefault();
        }
        public void Update(RawMaterial _object)
        {
            _ctx.RawMaterials.Update(_object);
            _ctx.SaveChanges();
        }
    }
}
