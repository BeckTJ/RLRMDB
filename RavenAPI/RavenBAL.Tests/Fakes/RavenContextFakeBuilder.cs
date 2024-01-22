using Microsoft.EntityFrameworkCore.ChangeTracking;
using RavenDB.Models;

namespace Service.Tests.Fakes
{
    public class RavenContextFakeBuilder
    {
        private MaterialVendor _milk;
        private List<MaterialVendor> _lemon;
        private List<MaterialVendor> _liquor;
        private List<MaterialVendor> _juice;

        private List<RawMaterial> _milkRM;
        private List<RawMaterial> _lemonRM;
        private List<RawMaterial> _juiceRM;
        private List<RawMaterial> _liquorRM;

        private List<SampleRequired> _milkSR;
        private List<SampleRequired> _lemonSR;
        private List<SampleRequired> _juiceSR;
        private List<SampleRequired> _liquorSR;

        private List<SampleSubmit> _milkSS;
        private List<SampleSubmit> _lemonSS;
        private List<SampleSubmit> _juiceSS;
        private List<SampleSubmit> _liquorSS;

        public MaterialVendor WithMaterialVendorMilk()
        {
            return new MaterialVendor(){
                ParentMaterialNumber = 58971,
                MaterialNumber = 3282571,
                VendorName = "Stop N Shop",
                MaterialCode = "AA",
                SequenceId = 800,
                TotalRecords = 100,
                UnitOfIssue = "kg",
                BatchManaged = true,
                ProcessOrderRequired = false,
            };
        }
        public RavenContextFakeBuilder WithMilkRawMaterial()
        {
            _milkRM.Add(new RawMaterial()
            {
                DrumLotNumber = "800AA4A01",
                MaterialNumber = 3282571,
                DrumWeight = 180,
                SapBatchNumber = null,
                ContainerNumber = null,
                InspectionLotNumber = null,
                SampleId = 123456,
                VendorLotNumber = "999-111-222",
            });
            _milkRM.Add(new RawMaterial()
            {
                DrumLotNumber = "802AA4A05",
                MaterialNumber = 3282571,
                DrumWeight = 180,
                SapBatchNumber = null,
                ContainerNumber = null,
                InspectionLotNumber = null,
                SampleId = 123456,
                VendorLotNumber = "999-111-222",
            });

            return this;
        }
        public RavenContextFakeBuilder WithMilkSampleSubmit()
        {
            _milkSS.Add(new SampleSubmit()
            {
                SampleId = 123456,
                SampleType = "RAW",
                InspectionLotNumber = null,
                SampleDate = new DateTime(2024,1,7),
                Approved = true,
                Rejected = false,
                ReviewDate = new DateTime(2024,1,9),
            });
            _milkSS.Add(new SampleSubmit()
            {
                SampleId = 234567,
                SampleType = "RAW",
                InspectionLotNumber = null,
                SampleDate = new DateTime(2024, 1, 7),
                Approved = false,
                Rejected = false,
                ReviewDate = null,
            });
            _milkSS.Add(new SampleSubmit()
            {
                SampleId = 999999,
                SampleType = "RAW",
                InspectionLotNumber = null,
                SampleDate = new DateTime(2024, 1, 10),
                Approved = false,
                Rejected = true,
                ReviewDate = new DateTime(2024, 1, 15),
            });
            return this;
        }
        public RavenContextFakeBuilder WithMilkSampleRequired()
        {
            _milkSR.Add(new SampleRequired()
            {
                MaterialNumber = 58971,
                MaterialType = "Finished Product",
                Vln = "Finished Product",
                Assay = true,
                Water = true,
                Metals = true,
                Chloride = true,
                Boron = false,
                Phosphorus = false,
            });
            return this;
        }
        public RavenContextFakeBuilder WithMaterialVendorLemon()
        {
            _lemon.Add(new MaterialVendor()
            {
                ParentMaterialNumber = 58143,
                MaterialNumber = 32409,
                VendorName = "Wine Palace",
                MaterialCode = "EA",
                SequenceId = 900,
                TotalRecords = 100,
                UnitOfIssue = "kg",
                BatchManaged = false,
                ProcessOrderRequired = false,
            });
            return this;
        }
        public RavenContextFakeBuilder WithMaterialVendorLiquor()
        {
            _liquor.Add(new MaterialVendor()
            {
                ParentMaterialNumber = 58765,
                MaterialNumber = 30173,
                VendorName = "Bevmo",
                MaterialCode = "FA",
                SequenceId = 100,
                TotalRecords = 100,
                UnitOfIssue = "kg",
                BatchManaged = true,
                ProcessOrderRequired = false,
            });
            return this;
        }
        public RavenContextFakeBuilder WithMaterialVendorJuice()
        {
            _juice.Add(new MaterialVendor()
            {
                ParentMaterialNumber = 58423,
                MaterialNumber = 3195356,
                VendorName = "Reclaim",
                MaterialCode = "BA",
                SequenceId = 1000,
                TotalRecords = 1000,
                UnitOfIssue = "kg",
                BatchManaged = false,
                ProcessOrderRequired = true,
            });
            return this;
        }
    }
}
