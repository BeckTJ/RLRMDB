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
    public class RepositoryMaterial : IRepository<Material>
    {
        private readonly RavenDBContext _ctx;
        public RepositoryMaterial(RavenDBContext ctx) => _ctx = ctx;
        public async Task<Material> Create(Material _object)
        {
            var obj = await _ctx.Materials.AddAsync(_object);
            _ctx.SaveChanges();
            return obj.Entity;
        }

        public void Delete(Material _object)
        {
            _ctx.Remove(_object);
            _ctx.SaveChanges();
        }

        public IEnumerable<Material> GetAll()
        {
            try
            {
                return _ctx.Materials.ToList();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Material GetById(int id)
        {
            return _ctx.Materials.Where(x => x.MaterialNumber == id).FirstOrDefault();
        }

        public void Update(Material _object)
        {
            _ctx.Materials.Update(_object);
            _ctx.SaveChanges();
        }
    }
}
