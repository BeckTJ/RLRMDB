using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RavenDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Tests.Unit.Fakes
{
    public class RavenContextFakeMaterialVendor
    {
        public MaterialVendor WithMaterialVendorBeer()
        {
            return new MaterialVendor
            {
                ParentMaterialNumber = 58423,
                MaterialNumber = 36178,
                VendorName = "Walmart",
                MaterialCode = "BA",
                SequenceId = 100,
                CurrentSequenceId = 100,
                TotalRecords = 100,
                UnitOfIssue = "kg",
                BatchManaged = false,
                ProcessOrderRequired = false,
                VendorLots = new List<VendorLot>()
                {
                    new VendorLot()
                    {
                        VendorLotNumber = "123ASD2345",
                        SampleId = 45678,
                        Quantity = 5,
                        MaterialNumber = 36178,
                        RawMaterials = 
                        {
                            new RawMaterial()
                            {
                                DrumLotNumber = "100BA4D06",
                                MaterialNumber = 36178,
                                DrumWeight = 180,
                                SampleId = 45678,
                            }
                        },
                    },
                    new VendorLot()
                    {
                        VendorLotNumber = "999ASD11111",
                        SampleId = null,
                        Quantity = 3,
                        MaterialNumber = 36178,
                    },
                }
            };
        }
        public MaterialVendor WithMaterialVendorJuice()
        {
            return new MaterialVendor
            {
                ParentMaterialNumber = 58245,
                MaterialNumber = 32716,
                VendorName = "Liquor Store",
                MaterialCode = "DA",
                SequenceId = 800,
                CurrentSequenceId = 899,
                TotalRecords = 100,
                UnitOfIssue = "kg",
                BatchManaged = false,
                VendorLots = new List<VendorLot>()
                {
                    new VendorLot()
                    {
                        VendorLotNumber = "999-111-222",
                        SampleId = 123456,
                        Quantity = 3,
                        MaterialNumber = 3282571,
                        RawMaterials =
                        {
                            new RawMaterial()
                            {
                                DrumLotNumber = "800DA4D06",
                                MaterialNumber = 32716,
                                DrumWeight = 180,
                                SampleId = 123456,
                            }
                        }
                    },
                    new VendorLot()
                    {
                        VendorLotNumber = "999-111-333",
                        SampleId = 11111,
                        Quantity = 3,
                        MaterialNumber = 3282571,
                    },
                }
            };

        }
    }
}
