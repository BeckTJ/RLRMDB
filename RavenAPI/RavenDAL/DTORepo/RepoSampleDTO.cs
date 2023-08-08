using RavenDAL.Data;
using RavenDAL.DTO;
using RavenDAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RavenDAL.DTORepo
{
    public class RepoSampleDTO : IRepository<SampleDTO>
    {
        private readonly RavenDBContext _ctx;

        public RepoSampleDTO(RavenDBContext ctx)
        {
            _ctx = ctx;
        }

        public Task<SampleDTO> Create(SampleDTO _object)
        {
            throw new NotImplementedException();
        }

        public void Delete(SampleDTO _object)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SampleDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public SampleDTO GetByInspectionLotNumber(long id)
        {
            return _ctx.SampleSubmits.Where(x => x.InspectionLotNumber == id)
            .Select(x => new SampleDTO
            {
                SampleSubmitNumber = x.SampleSubmitNumber,
                InspectionLotNumber = x.InspectionLotNumber,
                SampleDate = x.SampleDate,
                Rejected = x.Rejected,
                ReviewDate = x.ReviewDate,
                ExperationDate = x.ExperiationDate,
            }).FirstOrDefault();
        }
        public SampleDTO GetById(int id)
        {
            throw new NotImplementedException();
        }

        public SampleDTO GetBySampleNumber(string name)
        {
            return _ctx.SampleSubmits
                .Where(x => x.SampleSubmitNumber == name)
                .Select(x => new SampleDTO
                {
                    SampleSubmitNumber = x.SampleSubmitNumber,
                    InspectionLotNumber = x.InspectionLotNumber,
                    SampleDate = x.SampleDate,
                    Rejected = x.Rejected,
                    ReviewDate = x.ReviewDate,
                    ExperationDate = x.ExperiationDate,
                }).FirstOrDefault();
        }

        public void Update(SampleDTO _object)
        {
            throw new NotImplementedException();
        }

        public SampleDTO GetByLotNumber(string LotNumber)
        {
            throw new NotImplementedException();
        }
    }
}
