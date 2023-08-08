using RavenDAL.Data;
using RavenDAL.DTO;
using RavenDAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDAL.DTORepo
{
    public class RepoVendorBatchDTO : IVendor<VendorBatchDTO>
    {
        private readonly RavenDBContext _ctx;

        public RepoVendorBatchDTO(RavenDBContext ctx)
        {
            _ctx = ctx;
        }

        public Task<VendorBatchDTO> Create(VendorBatchDTO vendorLot)
        {
            throw new NotImplementedException();
        }

        public void Delete(VendorBatchDTO vendorLot)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VendorBatchDTO> GetAllVendorBatch(int materialNumber)
        {
            return _ctx.VendorBatches
                .Where(v => v.MaterialNumber == materialNumber)
                .Select(v => new VendorBatchDTO
                {
                    VendorLotNumber = v.VendorBatchNumber,
                    VendorName = v.VendorName,
                    MaterialNumber = (int)v.MaterialNumber,
                    SampleId = v.SampleSubmitNumber,
                    Quantity = v.Quantity,
                }).ToList();
        }

        public VendorBatchDTO GetVendorBatch(string vendorLotNumber)
        {
            return _ctx.VendorBatches
               .Where(x => x.VendorBatchNumber == vendorLotNumber)
               .Select(v => new VendorBatchDTO
               {
                   VendorLotNumber = v.VendorBatchNumber,
                   VendorName = v.VendorName,
                   MaterialNumber = (int)v.MaterialNumber,
                   SampleId = v.SampleSubmitNumber,
                   Quantity = v.Quantity,
               }).FirstOrDefault();
        }

        public void Update(VendorBatchDTO vendorLot)
        {
            throw new NotImplementedException();
        }
    }
}