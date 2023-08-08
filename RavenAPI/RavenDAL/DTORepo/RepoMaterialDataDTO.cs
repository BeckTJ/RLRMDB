using RavenDAL.Data;
using RavenDAL.DTO;
using RavenDAL.Interface;
using RavenDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDAL.DTORepo
{
    public class RepoMaterialDataDTO : IMaterialData<MaterialDataDTO>
    {
        private readonly RavenDBContext _ctx;
        public RepoMaterialDataDTO(RavenDBContext ctx) => _ctx = ctx;
        public Task<MaterialDataDTO> Create(MaterialDataDTO _object)
        {
            throw new NotImplementedException();
        }

        public void Delete(MaterialDataDTO _object)
        {
            throw new NotImplementedException();
        }
        public void Update(MaterialDataDTO _object)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MaterialDataDTO> GetAll()
        {
            return (from Material in _ctx.Materials
                    join MaterialNumber in _ctx.MaterialNumbers on Material.MaterialNumber equals MaterialNumber.ParentMaterialNumber
                    join MaterialId in _ctx.MaterialIds on Material.MaterialNumber equals MaterialId.MaterialNumber
                    where MaterialNumber.ParentMaterialNumber == Material.MaterialNumber
                    select new MaterialDataDTO
                    {
                        MaterialName = Material.MaterialName,
                        MaterialNumber = MaterialNumber.MaterialNumber1,
                        MaterialAbrev = Material.MaterialNameAbreviation,
                        SpecificGravity = Material.SpecificGravity,
                        MaterialCode = MaterialId.MaterialCode,
                        PermitNumber = Material.PermitNumber,
                        UnitOfIssue = MaterialNumber.UnitOfIssue,
                        BatchManaged = MaterialNumber.BatchManaged,
                        SequenceId = MaterialId.SequenceId,
                    }).ToList();
        }

        public MaterialDataDTO GetById(int materialNumber)
        {
            return (from Material in _ctx.Materials
                    join MaterialNumber in _ctx.MaterialNumbers on Material.MaterialNumber equals MaterialNumber.ParentMaterialNumber
                    join MaterialId in _ctx.MaterialIds on Material.MaterialNumber equals MaterialId.MaterialNumber
                    where MaterialNumber.MaterialNumber1 == materialNumber
                    select new MaterialDataDTO
                    {
                        MaterialName = Material.MaterialName,
                        MaterialNumber = MaterialNumber.MaterialNumber1,
                        MaterialAbrev = Material.MaterialNameAbreviation,
                        SpecificGravity = Material.SpecificGravity,
                        MaterialCode = MaterialId.MaterialCode,
                        PermitNumber = Material.PermitNumber,
                        UnitOfIssue = MaterialNumber.UnitOfIssue,
                        BatchManaged = MaterialNumber.BatchManaged,
                        SequenceId = MaterialId.SequenceId,
                    }).FirstOrDefault();
        }

        public MaterialDataDTO GetByLotNumber(string LotNumber)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<MaterialDataDTO> GetMaterialNumberFromParent(int parentMaterialNumber)
        {
            return _ctx.MaterialNumbers
                .Where(m => m.ParentMaterialNumber == parentMaterialNumber)
                .Select(m => new MaterialDataDTO
                {
                    MaterialNumber = m.MaterialNumber1,
                    BatchManaged = m.BatchManaged,
                }).ToList();
        }

    }
}
