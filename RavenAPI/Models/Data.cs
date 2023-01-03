using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RavenAPI.Models
{
    public partial class Data : DbContext
    {
        public Data()
        {
        }

        public Data(DbContextOptions<Data> options)
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
        public virtual DbSet<ProductNumberSequence> ProductNumberSequences { get; set; } = null!;
        public virtual DbSet<ProductRun> ProductRuns { get; set; } = null!;
        public virtual DbSet<Production> Productions { get; set; } = null!;
        public virtual DbSet<RawMaterial> RawMaterials { get; set; } = null!;
        public virtual DbSet<Receiver> Receivers { get; set; } = null!;
        public virtual DbSet<SampleRequired> SampleRequireds { get; set; } = null!;
        public virtual DbSet<SampleSubmit> SampleSubmits { get; set; } = null!;
        public virtual DbSet<SystemIndicator> SystemIndicators { get; set; } = null!;
        public virtual DbSet<SystemNomenclature> SystemNomenclatures { get; set; } = null!;
        public virtual DbSet<Vendor> Vendors { get; set; } = null!;
        public virtual DbSet<VendorBatch> VendorBatches { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=localhost; Initial Catalog=RavenDB; Persist Security Info=True;User Id = SA; Password=FR*@ger12");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlphabeticDate>(entity =>
            {
                entity.HasKey(e => e.MonthNumber)
                    .HasName("PK__Alphabet__C6DA02F15407DAA7");

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
                    .HasName("PK__Indicato__9394F68A64A48530");

                entity.ToTable("IndicatorSetPoint", "Engineering");

                entity.Property(e => e.Indicator)
                    .HasMaxLength(10)
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
                    .HasConstraintName("FK__Indicator__Mater__34C8D9D1");

                entity.HasOne(d => d.NomenclatureNavigation)
                    .WithMany(p => p.IndicatorSetPoints)
                    .HasForeignKey(d => d.Nomenclature)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Indicator__Nomen__35BCFE0A");
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.HasKey(e => e.MaterialNumber)
                    .HasName("PK__Material__E4D2E4BF7A23F0A5");

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

                entity.Property(e => e.ProductCode)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.RawMaterialCode)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.SpecificGravity).HasColumnType("decimal(3, 2)");
            });

            modelBuilder.Entity<MaterialId>(entity =>
            {
                entity.HasKey(e => new { e.MaterialNumber, e.VendorName })
                    .HasName("PK__Material__93E0EE8A80ABEE9B");

                entity.ToTable("MaterialId", "Materials");

                entity.Property(e => e.VendorName)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MaterialNumber>(entity =>
            {
                entity.HasKey(e => e.MaterialNumber1)
                    .HasName("PK__Material__E4D2E4BF940F3AE3");

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
                    .HasConstraintName("FK__MaterialN__Paren__3B75D760");
            });

            modelBuilder.Entity<PreStartCheck>(entity =>
            {
                entity.HasKey(e => e.CheckId)
                    .HasName("PK__PreStart__8681570672AB012F");

                entity.ToTable("PreStartChecks", "Distillation");

                entity.Property(e => e.CheckId).HasColumnName("CheckID");

                entity.Property(e => e.HeliumCylinderPsi).HasColumnName("HeliumCylinderPSI");

                entity.Property(e => e.HeliumFlowPsi).HasColumnName("HeliumFlowPSI");

                entity.Property(e => e.VacuumTrapInstallDate).HasColumnType("date");

                entity.HasOne(d => d.MaterialNumberNavigation)
                    .WithMany(p => p.PreStartChecks)
                    .HasForeignKey(d => d.MaterialNumber)
                    .HasConstraintName("FK__PreStartC__Mater__5535A963");
            });

            modelBuilder.Entity<ProductNumberSequence>(entity =>
            {
                entity.HasKey(e => e.SequenceId)
                    .HasName("PK__ProductN__BAD614918DC83037");

                entity.ToTable("ProductNumberSequence", "Distillation");
            });

            modelBuilder.Entity<ProductRun>(entity =>
            {
                entity.HasKey(e => e.RunId)
                    .HasName("PK__ProductR__A259D4DDE95A2846");

                entity.ToTable("ProductRun", "Distillation");

                entity.Property(e => e.DrumLotNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.KopotDrained).HasColumnName("KOPotDrained");

                entity.Property(e => e.SystemStatus)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.DrumLotNumberNavigation)
                    .WithMany(p => p.ProductRuns)
                    .HasForeignKey(d => d.DrumLotNumber)
                    .HasConstraintName("FK__ProductRu__DrumL__5CD6CB2B");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.ProductRuns)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__ProductRu__Emplo__5DCAEF64");
            });

            modelBuilder.Entity<Production>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK__Producti__B40CC6CDF705BC8D");

                entity.ToTable("Production", "Distillation");

                entity.HasIndex(e => e.ProcessOrder, "IX_Production_ProcessOrder");

                entity.HasIndex(e => e.ProductLotNumber, "IX_Production_ProductLotNumber");

                entity.HasIndex(e => e.ProductBatchNumber, "IX_Production_ProductionBatchNumber");

                entity.Property(e => e.ProcessOrder).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.ProductLotNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SampleSubmitNumber)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.MaterialNumberNavigation)
                    .WithMany(p => p.Productions)
                    .HasForeignKey(d => d.MaterialNumber)
                    .HasConstraintName("FK__Productio__Mater__5812160E");

                entity.HasOne(d => d.Receiver)
                    .WithMany(p => p.Productions)
                    .HasForeignKey(d => d.ReceiverId)
                    .HasConstraintName("FK__Productio__Recei__59063A47");

                entity.HasOne(d => d.SampleSubmitNumberNavigation)
                    .WithMany(p => p.Productions)
                    .HasForeignKey(d => d.SampleSubmitNumber)
                    .HasConstraintName("FK__Productio__Sampl__59FA5E80");
            });

            modelBuilder.Entity<RawMaterial>(entity =>
            {
                entity.HasKey(e => e.DrumLotNumber)
                    .HasName("PK__RawMater__64C9F8476FD4D475");

                entity.ToTable("RawMaterial", "Distillation");

                entity.Property(e => e.DrumLotNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ContainerNumber)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DateUsed).HasColumnType("date");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .IsFixedLength();

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
                    .HasConstraintName("FK__RawMateri__Emplo__52593CB8");

                entity.HasOne(d => d.MaterialNumberNavigation)
                    .WithMany(p => p.RawMaterials)
                    .HasForeignKey(d => d.MaterialNumber)
                    .HasConstraintName("FK__RawMateri__Mater__4F7CD00D");

                entity.HasOne(d => d.SampleSubmitNumberNavigation)
                    .WithMany(p => p.RawMaterials)
                    .HasForeignKey(d => d.SampleSubmitNumber)
                    .HasConstraintName("FK__RawMateri__Sampl__5070F446");

                entity.HasOne(d => d.VendorBatchNumberNavigation)
                    .WithMany(p => p.RawMaterials)
                    .HasForeignKey(d => d.VendorBatchNumber)
                    .HasConstraintName("FK__RawMateri__Vendo__5165187F");
            });

            modelBuilder.Entity<Receiver>(entity =>
            {
                entity.ToTable("Receiver", "Distillation");

                entity.Property(e => e.ReceiverName)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<SampleRequired>(entity =>
            {
                entity.HasKey(e => e.RequiredId)
                    .HasName("PK__SampleRe__2A9DE23026486236");

                entity.ToTable("SampleRequired", "QualityControl");

                entity.Property(e => e.NumberOfAmps).HasDefaultValueSql("((0))");

                entity.Property(e => e.NumberOfMetals).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.MaterialNumberNavigation)
                    .WithMany(p => p.SampleRequireds)
                    .HasForeignKey(d => d.MaterialNumber)
                    .HasConstraintName("FK__SampleReq__Mater__412EB0B6");
            });

            modelBuilder.Entity<SampleSubmit>(entity =>
            {
                entity.HasKey(e => e.SampleSubmitNumber)
                    .HasName("PK__SampleSu__DFFEFAA0769B51B3");

                entity.ToTable("SampleSubmit", "QualityControl");

                entity.Property(e => e.SampleSubmitNumber)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ApprovalDate).HasColumnType("date");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ExperiationDate).HasColumnType("date");

                entity.Property(e => e.RejectedDate).HasColumnType("date");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.SampleSubmits)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__SampleSub__Emplo__38996AB5");
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
                    .HasName("PK__SystemNo__8F9251AFDD03E0F0");

                entity.ToTable("SystemNomenclature", "Engineering");

                entity.Property(e => e.Nomenclature)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.HasKey(e => e.VendorName)
                    .HasName("PK__Vendor__7320A356B948D429");

                entity.ToTable("Vendor", "Vendors");

                entity.Property(e => e.VendorName)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.IsMpps).HasColumnName("IsMPPS");
            });

            modelBuilder.Entity<VendorBatch>(entity =>
            {
                entity.HasKey(e => e.VendorBatchNumber)
                    .HasName("PK__VendorBa__4E4125DBCA30B61B");

                entity.ToTable("VendorBatch", "Vendors");

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
                    .HasConstraintName("FK__VendorBat__Mater__4AB81AF0");

                entity.HasOne(d => d.SampleSubmitNumberNavigation)
                    .WithMany(p => p.VendorBatches)
                    .HasForeignKey(d => d.SampleSubmitNumber)
                    .HasConstraintName("FK__VendorBat__Sampl__49C3F6B7");

                entity.HasOne(d => d.VendorNameNavigation)
                    .WithMany(p => p.VendorBatches)
                    .HasForeignKey(d => d.VendorName)
                    .HasConstraintName("FK__VendorBat__Vendo__48CFD27E");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
