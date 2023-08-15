using RavenBAL.Interface;
using RavenBAL.src;
using RavenDAL.DTO;
using RavenDAL.DTORepo;
using RavenDAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenBAL.Repository
{
    public class RepoVendorLot : IVendorLot<VendorLot>
    {
        private readonly IVendor<VendorBatchDTO> _vendorBatchDTO;
        private readonly IRawMaterialDrum<RawMaterialDrum> _rawMaterial;
        private readonly ISample<Sample> _sample;

        public RepoVendorLot( IVendor<VendorBatchDTO> vendorBatch, IRawMaterialDrum<RawMaterialDrum> rawMaterial,ISample<Sample> sample)
        {
            _vendorBatchDTO = vendorBatch;
            _rawMaterial = rawMaterial;
            _sample = sample;
        }
        public IEnumerable<VendorLot> GetAll(int materialNumber)
        {
            throw new NotImplementedException();            
        }
        public VendorLot GetVendorLot(string lotNumber)
        {
            var batch = _vendorBatchDTO.GetVendorBatch(lotNumber);

            return new VendorLot
            {
                MaterialNumber = batch.MaterialNumber,
                LotNumber = batch.VendorLotNumber,
                Quantity = (int)batch.Quantity,
                SampleId = batch.SampleId,
                RawMaterialDrums = _rawMaterial.GetApprovedRawMaterial(batch.MaterialNumber),
            };
        }
    }
}
