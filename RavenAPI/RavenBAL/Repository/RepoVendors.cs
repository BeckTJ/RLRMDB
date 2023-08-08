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
        private readonly IRawMaterialDrum<RawMaterialDrumDTO> _rawMaterial;

        public RepoVendorLot( IVendor<VendorBatchDTO> vendorBatch, IRawMaterialDrum<RawMaterialDrumDTO> rawMaterial)
        {
            _vendorBatchDTO = vendorBatch;
            _rawMaterial = rawMaterial;
        }

        public IEnumerable<VendorLot> GetAll(int materialNumber)
        {
            List<VendorLot> vendorLot = new List<VendorLot>();
            List <VendorBatchDTO> vendorBatch = new List<VendorBatchDTO>();
            vendorBatch = (List<VendorBatchDTO>)_vendorBatchDTO.GetAllVendorBatch(materialNumber);
            
            foreach(var vendor in vendorBatch)
            {
                vendorLot.Add(new VendorLot
                {
                    VendorBatch = vendor,
                    RawMaterialDrum = _rawMaterial.GetAllByVendorLotNumber(vendor.VendorLotNumber),
                });
            }
            return vendorLot;
        }

        public VendorLot GetVendorLot(string LotNumber)
        {
            return new() 
            {
                VendorBatch = (VendorBatchDTO)_vendorBatchDTO.GetVendorBatch(LotNumber),
                RawMaterialDrum = _rawMaterial.GetAllByVendorLotNumber(LotNumber),
            };
        }
    }
}
