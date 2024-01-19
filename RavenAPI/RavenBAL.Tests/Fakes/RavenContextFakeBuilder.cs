using Microsoft.EntityFrameworkCore.ChangeTracking;
using RavenDB.Models;

namespace Service.Tests.Fakes
{
    public class RavenContextFakeBuilder
    {
        private readonly RavenContextFake _ctx = new();
        private EntityEntry<MaterialVendor> _milk;
        private EntityEntry<MaterialVendor> _lemon;
        private EntityEntry<MaterialVendor> _liquor;
        private EntityEntry<MaterialVendor> _juice;

        private EntityEntry<RawMaterial> _milkRM;
        private EntityEntry<RawMaterial> _lemonRM;
        private EntityEntry<RawMaterial> _juiceRM;
        private EntityEntry<RawMaterial> _liquorRM;

        private EntityEntry<SampleRequired> _milkSR;
        private EntityEntry<SampleRequired> _lemonSR;
        private EntityEntry<SampleRequired> _juiceSR;
        private EntityEntry<SampleRequired> _liquorSR;

        private EntityEntry<SampleSubmit> _milkSS;
        private EntityEntry<SampleSubmit> _lemonSS;
        private EntityEntry<SampleSubmit> _juiceSS;
        private EntityEntry<SampleSubmit> _liquorSS;

        public RavenContextFakeBuilder WithMaterialVendorMilk()
        {
            _milk = _ctx.Add(new MaterialVendor(){
                ParentMaterialNumber = 58971,
                MaterialNumber = 3282571,
                VendorName = "Stop N Shop",
                MaterialCode = "AA",
                SequenceId = 800,
                TotalRecords = 100,
                UnitOfIssue = "kg",
                BatchManaged = true,
                ProcessOrderRequired = false,
            });
            return this;
        }
        public RavenContextFakeBuilder WithMilkRawMaterial()
        {
            _milkRM = _ctx.Add(new RawMaterial()
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
            _milkRM = _ctx.Add(new RawMaterial()
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
            _milkSS = _ctx.Add(new SampleSubmit()
            {
                SampleId = 123456,
                SampleType = "RAW",
                InspectionLotNumber = null,
                SampleDate = new DateTime(2024,1,7),
                Approved = true,
                Rejected = false,
                ReviewDate = new DateTime(2024,1,9),
            });
            _milkSS = _ctx.Add(new SampleSubmit()
            {
                SampleId = 234567,
                SampleType = "RAW",
                InspectionLotNumber = null,
                SampleDate = new DateTime(2024, 1, 7),
                Approved = false,
                Rejected = false,
                ReviewDate = null,
            });
            _milkSS = _ctx.Add(new SampleSubmit()
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
            _milkSR = _ctx.Add(new SampleRequired()
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
            _lemon = _ctx.Add(new MaterialVendor()
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
            _liquor = _ctx.Add(new MaterialVendor()
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
            _juice = _ctx.Add(new MaterialVendor()
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
        public RavenContextFake Build()
        {
            _ctx.SaveChanges();
            return _ctx;
        }
        public void Dispose()
        {
            _ctx.Database.EnsureDeleted();
            _ctx.Dispose();
        }
    }
}
