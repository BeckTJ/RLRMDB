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
    public class RepositorySampleSubmit : IRepository<SampleSubmit>
    {
        RavenDBContext _ctx;
        public RepositorySampleSubmit(RavenDBContext ctx) => _ctx = new RavenDBContext();
        public async Task<SampleSubmit> Create(SampleSubmit _object)
        {
            var obj = await _ctx.SampleSubmits.AddAsync(_object);
            _ctx.SaveChanges();
            return obj.Entity;
        }
        public void Delete(SampleSubmit _object)
        {
            _ctx.Remove(_object);
            _ctx.SaveChanges();
        }

        public IEnumerable<SampleSubmit> GetAll()
        {
            try
            {
                return _ctx.SampleSubmits.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public SampleSubmit GetById(int id)
        {
            return _ctx.SampleSubmits.Where(x => x.InspectionLotNumber == id).FirstOrDefault();
        }

        public void Update(SampleSubmit _object)
        {
            _ctx.SampleSubmits.Update(_object);
            _ctx.SaveChanges();
        }
    }
}
