using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RavenAPI.Models
{
    public partial class RavenDBContext : DbContext
    {
        public RavenDBContext()
        {
        }

        public RavenDBContext(DbContextOptions<RavenDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AlphabeticDate> AlphabeticDates { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<IndicatorSetPoint> IndicatorSetPoints { get; set; } = null!;
        public virtual DbSet<Material> Materials { get; set; } = null!;
        public virtual DbSet<MaterialId> MaterialIds { get; set; } = null!;
        public virtual DbSet<MaterialNumber> MaterialNumbers { get; set; } = null!;
        public virtual DbSet<PreStartCheck> PreStartChecks { get; set; } = null!;
        public virtual DbSet<ProductLevel> ProductLevels { get; set; } = null!;
        public virtual DbSet<ProductRun> ProductRuns { get; set; } = null!;
        public virtual DbSet<Production> Productions { get; set; } = null!;
        public virtual DbSet<RawMaterial> RawMaterials { get; set; } = null!;
        public virtual DbSet<Receiver> Receivers { get; set; } = null!;
        public virtual DbSet<SampleRequired> SampleRequireds { get; set; } = null!;
        public virtual DbSet<SampleSubmit> SampleSubmits { get; set; } = null!;
        public virtual DbSet<SystemIndicator> SystemIndicators { get; set; } = null!;
        public virtual DbSet<SystemNomenclature> SystemNomenclatures { get; set; } = null!;
        public virtual DbSet<SystemReceiver> SystemReceivers { get; set; } = null!;
        public virtual DbSet<SystemStatus> SystemStatuses { get; set; } = null!;
        public virtual DbSet<UnitOfIssue> UnitOfIssues { get; set; } = null!;
        public virtual DbSet<Vendor> Vendors { get; set; } = null!;
        public virtual DbSet<VendorBatch> VendorBatches { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source= Localhost;Initial Catalog = RavenDB; Persist Security Info = True; User Id = SA; Password = FR*@ger12;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlphabeticDate>(entity =>
            {
                entity.HasKey(e => e.MonthNumber)
                    .HasName("PK__Alphabet__C6DA02F1380440E8");

                entity.ToTable("AlphabeticDate", "Distillation");

                entity.Property(e => e.MonthNumber).ValueGeneratedNever();

                entity.Property(e => e.AlphabeticCode)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee", "HumanResources");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.FirstName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.LastName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<IndicatorSetPoint>(entity =>
            {
                entity.HasKey(e => e.SystemId)
                    .HasName("PK__Indicato__9394F68ABCEC5C92");

                entity.ToTable("IndicatorSetPoint", "Engineering");

                entity.Property(e => e.Indicator)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.IndicatorType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nomenclature)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SetPoint).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.Variance).HasColumnType("decimal(6, 2)");

                entity.HasOne(d => d.MaterialNumberNavigation)
                    .WithMany(p => p.IndicatorSetPoints)
                    .HasForeignKey(d => d.MaterialNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Indicator__Mater__398D8EEE");

                entity.HasOne(d => d.NomenclatureNavigation)
                    .WithMany(p => p.IndicatorSetPoints)
                    .HasForeignKey(d => d.Nomenclature)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Indicator__Nomen__3A81B327");
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.HasKey(e => e.MaterialNumber)
                    .HasName("PK__Material__E4D2E4BF2B2709D8");

                entity.ToTable("Material", "Materials");

                entity.HasIndex(e => e.MaterialNameAbreviation, "IX_Material_NameAbreviation");

                entity.Property(e => e.MaterialNumber).ValueGeneratedNever();

                entity.Property(e => e.CarbonDrumInstallDate).HasColumnType("date");

                entity.Property(e => e.CollectRefluxRatio)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.MaterialName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MaterialNameAbreviation)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.PermitNumber)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.PrefractionRefluxRatio)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.SpecificGravity).HasColumnType("decimal(3, 2)");

                entity.Property(e => e.VacuumTrapInstallDate).HasColumnType("date");
            });

            modelBuilder.Entity<MaterialId>(entity =>
            {
                entity.HasKey(e => new { e.MaterialNumber, e.VendorName })
                    .HasName("PK__Material__93E0EE8A2E679012");

                entity.ToTable("MaterialId", "Materials");

                entity.Property(e => e.VendorName)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.MaterialCode)
                    .HasMaxLength(3)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MaterialNumber>(entity =>
            {
                entity.HasKey(e => e.MaterialNumber1)
                    .HasName("PK__Material__E4D2E4BF7FEEDE3F");

                entity.ToTable("MaterialNumber", "Materials");

                entity.Property(e => e.MaterialNumber1)
                    .ValueGeneratedNever()
                    .HasColumnName("MaterialNumber");

                entity.Property(e => e.UnitOfIssue)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.HasOne(d => d.ParentMaterialNumberNavigation)
                    .WithMany(p => p.MaterialNumbers)
                    .HasForeignKey(d => d.ParentMaterialNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MaterialN__Paren__403A8C7D");
            });

            modelBuilder.Entity<PreStartCheck>(entity =>
            {
                entity.HasKey(e => e.CheckId)
                    .HasName("PK__PreStart__86815706192A5D04");

                entity.ToTable("PreStartChecks", "Distillation");

                entity.Property(e => e.CheckId).HasColumnName("CheckID");

                entity.Property(e => e.HeliumCylinderPsi).HasColumnName("HeliumCylinderPSI");

                entity.Property(e => e.HeliumFlowPsi).HasColumnName("HeliumFlowPSI");

                entity.HasOne(d => d.MaterialNumberNavigation)
                    .WithMany(p => p.PreStartChecks)
                    .HasForeignKey(d => d.MaterialNumber)
                    .HasConstraintName("FK__PreStartC__Mater__693CA210");

                entity.HasOne(d => d.Run)
                    .WithMany(p => p.PreStartChecks)
                    .HasForeignKey(d => d.RunId)
                    .HasConstraintName("FK__PreStartC__RunId__68487DD7");
            });

            modelBuilder.Entity<ProductLevel>(entity =>
            {
                entity.HasKey(e => e.LevelId)
                    .HasName("PK__ProductL__09F03C261BA5D351");

                entity.ToTable("ProductLevels", "Distillation");

                entity.Property(e => e.ProductLotNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SystemStatus)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.VisualVerification).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.ProductLotNumberNavigation)
                    .WithMany(p => p.ProductLevels)
                    .HasForeignKey(d => d.ProductLotNumber)
                    .HasConstraintName("FK__ProductLe__Produ__6383C8BA");

                entity.HasOne(d => d.RunNumberNavigation)
                    .WithMany(p => p.ProductLevels)
                    .HasForeignKey(d => d.RunNumber)
                    .HasConstraintName("FK__ProductLe__RunNu__6477ECF3");
            });

            modelBuilder.Entity<ProductRun>(entity =>
            {
                entity.HasKey(e => e.RunId)
                    .HasName("PK__ProductR__A259D4DD89F80AD2");

                entity.ToTable("ProductRun", "Distillation");

                entity.Property(e => e.DrumLotNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ProductLotNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RunStartDate).HasColumnType("date");

                entity.HasOne(d => d.DrumLotNumberNavigation)
                    .WithMany(p => p.ProductRuns)
                    .HasForeignKey(d => d.DrumLotNumber)
                    .HasConstraintName("FK__ProductRu__DrumL__5EBF139D");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.ProductRuns)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__ProductRu__Emplo__60A75C0F");

                entity.HasOne(d => d.ProductLotNumberNavigation)
                    .WithMany(p => p.ProductRuns)
                    .HasForeignKey(d => d.ProductLotNumber)
                    .HasConstraintName("FK__ProductRu__Produ__5FB337D6");
            });

            modelBuilder.Entity<Production>(entity =>
            {
                entity.HasKey(e => e.ProductLotNumber)
                    .HasName("PK__Producti__36A97022B8BC1EA6");

                entity.ToTable("Production", "Distillation");

                entity.HasIndex(e => e.ProcessOrder, "IX_Production_ProcessOrder");

                entity.HasIndex(e => e.ProductLotNumber, "IX_Production_ProductLotNumber");

                entity.HasIndex(e => e.ProductBatchNumber, "IX_Production_ProductionBatchNumber");

                entity.Property(e => e.ProductLotNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.InspectionLotNumber).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.ProcessOrder).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.ReceiverName)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.SampleSubmitNumber)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.MaterialNumberNavigation)
                    .WithMany(p => p.Productions)
                    .HasForeignKey(d => d.MaterialNumber)
                    .HasConstraintName("FK__Productio__Mater__59FA5E80");

                entity.HasOne(d => d.ReceiverNameNavigation)
                    .WithMany(p => p.Productions)
                    .HasForeignKey(d => d.ReceiverName)
                    .HasConstraintName("FK__Productio__Recei__5AEE82B9");

                entity.HasOne(d => d.SampleSubmitNumberNavigation)
                    .WithMany(p => p.Productions)
                    .HasForeignKey(d => d.SampleSubmitNumber)
                    .HasConstraintName("FK__Productio__Sampl__5BE2A6F2");
            });

            modelBuilder.Entity<RawMaterial>(entity =>
            {
                entity.HasKey(e => e.DrumLotNumber)
                    .HasName("PK__RawMater__64C9F84786AD37B9");

                entity.ToTable("RawMaterial", "Distillation");

                entity.Property(e => e.DrumLotNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ContainerNumber)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.InspectionLotNumber).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.SampleSubmitNumber)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.VendorBatchNumber)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.RawMaterials)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__RawMateri__Emplo__571DF1D5");

                entity.HasOne(d => d.MaterialNumberNavigation)
                    .WithMany(p => p.RawMaterials)
                    .HasForeignKey(d => d.MaterialNumber)
                    .HasConstraintName("FK__RawMateri__Mater__5441852A");

                entity.HasOne(d => d.SampleSubmitNumberNavigation)
                    .WithMany(p => p.RawMaterials)
                    .HasForeignKey(d => d.SampleSubmitNumber)
                    .HasConstraintName("FK__RawMateri__Sampl__5535A963");

                entity.HasOne(d => d.VendorBatchNumberNavigation)
                    .WithMany(p => p.RawMaterials)
                    .HasForeignKey(d => d.VendorBatchNumber)
                    .HasConstraintName("FK__RawMateri__Vendo__5629CD9C");
            });

            modelBuilder.Entity<Receiver>(entity =>
            {
                entity.HasKey(e => e.ReceiverName)
                    .HasName("PK__Receiver__FD2F0564A4BF896D");

                entity.ToTable("Receiver", "Engineering");

                entity.Property(e => e.ReceiverName)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<SampleRequired>(entity =>
            {
                entity.HasKey(e => new { e.MaterialNumber, e.Vln })
                    .HasName("PK__SampleRe__088F292ED8284CA5");

                entity.ToTable("SampleRequired", "QualityControl");

                entity.Property(e => e.Vln)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("VLN");

                entity.Property(e => e.AmpUnitOfIssue)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Amps).HasDefaultValueSql("((0))");

                entity.Property(e => e.Assay).HasDefaultValueSql("((0))");

                entity.Property(e => e.AssayBulb).HasDefaultValueSql("((0))");

                entity.Property(e => e.Boron).HasDefaultValueSql("((0))");

                entity.Property(e => e.BubblerUnitOfIssue)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Chloride).HasDefaultValueSql("((0))");

                entity.Property(e => e.MaterialType)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.MetalBubbler).HasDefaultValueSql("((0))");

                entity.Property(e => e.Metals).HasDefaultValueSql("((0))");

                entity.Property(e => e.Phosphorus).HasDefaultValueSql("((0))");

                entity.Property(e => e.Retain).HasDefaultValueSql("((0))");

                entity.Property(e => e.VialUnitOfIssue)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Vials).HasDefaultValueSql("((0))");

                entity.Property(e => e.Water).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<SampleSubmit>(entity =>
            {
                entity.HasKey(e => e.SampleSubmitNumber)
                    .HasName("PK__SampleSu__DFFEFAA0324DDCB2");

                entity.ToTable("SampleSubmit", "QualityControl");

                entity.Property(e => e.SampleSubmitNumber)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ExperiationDate).HasColumnType("date");

                entity.Property(e => e.ReviewDate).HasColumnType("date");

                entity.Property(e => e.SampleDate).HasColumnType("date");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.SampleSubmits)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__SampleSub__Emplo__3D5E1FD2");
            });

            modelBuilder.Entity<SystemIndicator>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SystemIndicator", "Engineering");

                entity.Property(e => e.IndicatorType)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SystemNomenclature>(entity =>
            {
                entity.HasKey(e => e.Nomenclature)
                    .HasName("PK__SystemNo__8F9251AF8FB82E8C");

                entity.ToTable("SystemNomenclature", "Engineering");

                entity.Property(e => e.Nomenclature)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SystemReceiver>(entity =>
            {
                entity.HasKey(e => e.ReceiverId)
                    .HasName("PK__SystemRe__FEBB5F276D18E8F5");

                entity.ToTable("SystemReceivers", "Engineering");

                entity.Property(e => e.ReceiverName)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.MaterialNumberNavigation)
                    .WithMany(p => p.SystemReceivers)
                    .HasForeignKey(d => d.MaterialNumber)
                    .HasConstraintName("FK__SystemRec__Mater__34C8D9D1");

                entity.HasOne(d => d.ReceiverNameNavigation)
                    .WithMany(p => p.SystemReceivers)
                    .HasForeignKey(d => d.ReceiverName)
                    .HasConstraintName("FK__SystemRec__Recei__35BCFE0A");
            });

            modelBuilder.Entity<SystemStatus>(entity =>
            {
                entity.HasKey(e => e.StatusCode)
                    .HasName("PK__SystemSt__6A7B44FDCF403068");

                entity.ToTable("SystemStatus", "Distillation");

                entity.Property(e => e.StatusCode)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.StatusName)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UnitOfIssue>(entity =>
            {
                entity.HasKey(e => e.UnitOfIssue1)
                    .HasName("PK__UnitOfIs__B79EB8A2D27511DD");

                entity.ToTable("UnitOfIssue", "Materials");

                entity.Property(e => e.UnitOfIssue1)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("UnitOfIssue");

                entity.Property(e => e.Nomenclature)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.HasKey(e => e.VendorName)
                    .HasName("PK__Vendor__7320A35654F1FC70");

                entity.ToTable("Vendor", "Materials");

                entity.Property(e => e.VendorName)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VendorBatch>(entity =>
            {
                entity.HasKey(e => e.VendorBatchNumber)
                    .HasName("PK__VendorBa__4E4125DBBC8F3CFD");

                entity.ToTable("VendorBatch", "Materials");

                entity.Property(e => e.VendorBatchNumber)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.SampleSubmitNumber)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.VendorName)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.MaterialNumberNavigation)
                    .WithMany(p => p.VendorBatches)
                    .HasForeignKey(d => d.MaterialNumber)
                    .HasConstraintName("FK__VendorBat__Mater__4F7CD00D");

                entity.HasOne(d => d.SampleSubmitNumberNavigation)
                    .WithMany(p => p.VendorBatches)
                    .HasForeignKey(d => d.SampleSubmitNumber)
                    .HasConstraintName("FK__VendorBat__Sampl__4E88ABD4");

                entity.HasOne(d => d.VendorNameNavigation)
                    .WithMany(p => p.VendorBatches)
                    .HasForeignKey(d => d.VendorName)
                    .HasConstraintName("FK__VendorBat__Vendo__4D94879B");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
