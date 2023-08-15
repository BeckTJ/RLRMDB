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
    public class RepoSampleDTO : ISampleSubmit<SampleDTO>
    {
        private readonly RavenDBContext _ctx;

        public RepoSampleDTO(RavenDBContext ctx)
        {
            _ctx = ctx;
        }

        public Task<SampleDTO> SubmitSample(SampleDTO _object)
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
                Approved = x.Approved,
                Rejected = x.Rejected,
                ReviewDate = x.ReviewDate,
                ExperationDate = x.ExperiationDate,
            }).FirstOrDefault();
        }
        public SampleDTO GetBySampleNumber(string sampleId)
        {
            return _ctx.SampleSubmits
                .Where(x => x.SampleSubmitNumber == sampleId)
                .Select(x => new SampleDTO
                {
                    SampleSubmitNumber = x.SampleSubmitNumber,
                    InspectionLotNumber = x.InspectionLotNumber,
                    SampleDate = x.SampleDate,
                    Approved = x.Approved,
                    Rejected = x.Rejected,
                    ReviewDate = x.ReviewDate,
                    ExperationDate = x.ExperiationDate,
                }).FirstOrDefault();
        }
    }
}
