using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RLRMBL.Models;

namespace RLRMBL.Context
{
    public partial class RLRMDBContext : DbContext
    {
        public RLRMDBContext()
        {
        }

        public RLRMDBContext(DbContextOptions<RLRMDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AlphabeticDate> AlphabeticDates { get; set; } = null!;
        public virtual DbSet<Distilation> Distilations { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Material> Materials { get; set; } = null!;
        public virtual DbSet<MaterialId> MaterialIds { get; set; } = null!;
        public virtual DbSet<MaterialNumber> MaterialNumbers { get; set; } = null!;
        public virtual DbSet<Production> Production { get; set; } = null!;
        public virtual DbSet<ProductNumberSequence> ProductNumberSequences { get; set; } = null!;
        public virtual DbSet<QualityControl> QualityControls { get; set; } = null!;
        public virtual DbSet<RawMaterial> RawMaterials { get; set; } = null!;
        public virtual DbSet<Receiver> Receivers { get; set; } = null!;
        public virtual DbSet<Vendor> Vendors { get; set; } = null!;
        public virtual DbSet<VendorBatchInformation> VendorBatchInformations { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source = localhost; Initial Catalog = RLRMDB; TrustServerCertificate = true; Persist Security Info = True; User ID = SA; Password = FR*@ger12");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlphabeticDate>(entity =>
            {
                entity.HasKey(e => e.MonthNumber)
                    .HasName("PK__alphabet__82D52291379915E9");

                entity.ToTable("alphabeticDate");

                entity.Property(e => e.MonthNumber)
                    .ValueGeneratedNever()
                    .HasColumnName("monthNumber");

                entity.Property(e => e.AlphabeticCode)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("alphabeticCode")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Distilation>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK__distilat__2D10D16A8BDBBBCE");

                entity.ToTable("distilation");

                entity.Property(e => e.ProductId).HasColumnName("productId");

                entity.Property(e => e.DrumLotNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("drumLotNumber")
                    .IsFixedLength();

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("endDate");

                entity.Property(e => e.Heels).HasColumnName("heels");

                entity.Property(e => e.HeelsPumped).HasColumnName("heelsPumped")
                    .IsRequired();

                entity.Property(e => e.Prefraction).HasColumnName("prefraction");

                entity.Property(e => e.ProductLotNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("productLotNumber")
                    .IsFixedLength();

                entity.Property(e => e.RawMaterialLoaded).HasColumnName("rawMaterialLoaded");

                entity.Property(e => e.RunNumber).HasColumnName("runNumber");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("startDate");

                entity.HasOne(d => d.DrumLotNumberNavigation)
                    .WithMany(p => p.Distilations)
                    .HasForeignKey(d => d.DrumLotNumber)
                    .HasConstraintName("FK__distilati__drumL__403A8C7D");

                entity.HasOne(d => d.ProductLotNumberNavigation)
                    .WithMany(p => p.Distilations)
                    .HasForeignKey(d => d.ProductLotNumber)
                    .HasConstraintName("FK__distilati__produ__3F466844");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employee");

                entity.Property(e => e.EmployeeId)
                    .ValueGeneratedNever()
                    .HasColumnName("employeeId");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("firstName")
                    .IsFixedLength();

                entity.Property(e => e.LastName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("lastName")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.HasKey(e => e.MaterialNameId)
                    .HasName("PK__material__6A6E3BCD2C8F556A");

                entity.ToTable("material");

                entity.Property(e => e.MaterialNameId).HasColumnName("materialNameId");

                entity.Property(e => e.CarbonDrumDaysAllowed).HasColumnName("carbonDrumDaysAllowed");

                entity.Property(e => e.CarbonDrumRequired).HasColumnName("carbonDrumRequired")
                    .IsRequired();

                entity.Property(e => e.CarbonDrumWeightAllowed).HasColumnName("carbonDrumWeightAllowed");

                entity.Property(e => e.MaterialName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("materialName")
                    .IsRequired();

                entity.Property(e => e.MaterialNameAbreviation)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("materialNameAbreviation")
                    .IsRequired();

                entity.Property(e => e.PermitNumber)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("permitNumber");

                entity.Property(e => e.ProductCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("productCode");

                entity.Property(e => e.RawMaterialCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("rawMaterialCode");
            });

            modelBuilder.Entity<MaterialId>(entity =>
            {
                entity.HasKey(e => e.MaterialId1)
                    .HasName("PK__material__99B653FDF99C4B8E");

                entity.ToTable("materialId");

                entity.Property(e => e.MaterialId1).HasColumnName("materialId");

                entity.Property(e => e.CurrentSequenceId).HasColumnName("currentSequenceId");

                entity.Property(e => e.MaterialNumber).HasColumnName("materialNumber");

                entity.Property(e => e.SequenceId).HasColumnName("sequenceId");

                entity.Property(e => e.VendorId).HasColumnName("vendorId");

                entity.HasOne(d => d.MaterialNumberNavigation)
                    .WithMany(p => p.MaterialIds)
                    .HasForeignKey(d => d.MaterialNumber)
                    .HasConstraintName("FK__materialI__mater__32E0915F");
            });

            modelBuilder.Entity<MaterialNumber>(entity =>
            {
                entity.HasKey(e => e.MaterialNumber1)
                    .HasName("PK__material__EE13FB891D767955");

                entity.ToTable("materialNumber");

                entity.Property(e => e.MaterialNumber1)
                    .ValueGeneratedNever()
                    .HasColumnName("materialNumber");

                entity.Property(e => e.BatchManaged).HasColumnName("batchManaged")
                    .IsRequired();

                entity.Property(e => e.IsRawMaterial).HasColumnName("isRawMaterial")
                    .IsRequired();

                entity.Property(e => e.MaterialNameId).HasColumnName("materialNameId")
                    .IsRequired();

                entity.Property(e => e.RequiresProcessOrder).HasColumnName("requiresProcessOrder")
                    .IsRequired();

                entity.Property(e => e.UnitOfIssue)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("unitOfIssue")
                    .IsFixedLength();

                entity.HasOne(d => d.MaterialName)
                    .WithMany(p => p.MaterialNumbers)
                    .HasForeignKey(d => d.MaterialNameId)
                    .HasConstraintName("FK__materialN__mater__300424B4");
            });

            modelBuilder.Entity<Production>(entity =>
            {
                entity.HasKey(e => e.ProductLotNumber)
                    .HasName("PK__product__794840451FC26F46");

                entity.ToTable("product");

                entity.Property(e => e.ProductLotNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("productLotNumber")
                    .IsFixedLength();

                entity.Property(e => e.MaterialNumber).HasColumnName("materialNumber");

                entity.Property(e => e.ProcessOrder)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("processOrder");

                entity.Property(e => e.ProductionBatchNumber).HasColumnName("productionBatchNumber");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.ReceiverId).HasColumnName("receiverId");

                entity.Property(e => e.SampleSubmitNumber)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("sampleSubmitNumber")
                    .IsFixedLength();

                entity.HasOne(d => d.MaterialNumberNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.MaterialNumber)
                    .HasConstraintName("FK__product__materia__3B75D760");

                entity.HasOne(d => d.SampleSubmitNumberNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SampleSubmitNumber)
                    .HasConstraintName("FK__product__sampleS__3C69FB99");
            });

            modelBuilder.Entity<ProductNumberSequence>(entity =>
            {
                entity.HasKey(e => e.SequenceId)
                    .HasName("PK__productN__53F4086332C9F781");

                entity.ToTable("productNumberSequence");

                entity.Property(e => e.SequenceId)
                    .ValueGeneratedNever()
                    .HasColumnName("sequenceId");

                entity.Property(e => e.SequenceIdEnd).HasColumnName("sequenceIdEnd");

                entity.Property(e => e.SequenceIdStart).HasColumnName("sequenceIdStart");
            });

            modelBuilder.Entity<QualityControl>(entity =>
            {
                entity.HasKey(e => e.SampleSubmitNumber)
                    .HasName("PK__qualityC__45525A7E70D8374B");

                entity.ToTable("qualityControl");

                entity.Property(e => e.SampleSubmitNumber)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("sampleSubmitNumber")
                    .IsFixedLength();

                entity.Property(e => e.ApprovalDate)
                    .HasColumnType("date")
                    .HasColumnName("approvalDate");

                entity.Property(e => e.ExperiationDate)
                    .HasColumnType("date")
                    .HasColumnName("experiationDate");

                entity.Property(e => e.InspectionLotNumber)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("inspectionLotNumber");

                entity.Property(e => e.Rejected).HasColumnName("rejected")
                    .IsRequired();

                entity.Property(e => e.RejectedDate)
                    .HasColumnType("date")
                    .HasColumnName("rejectedDate");

                entity.Property(e => e.VendorBatchId).HasColumnName("vendorBatchId");

                entity.HasOne(d => d.VendorBatch)
                    .WithMany(p => p.QualityControls)
                    .HasForeignKey(d => d.VendorBatchId)
                    .HasConstraintName("FK__qualityCo__vendo__2B3F6F97");
            });

            modelBuilder.Entity<RawMaterial>(entity =>
            {
                entity.HasKey(e => e.DrumLotNumber)
                    .HasName("PK__rawMater__944152908680F7FA");

                entity.ToTable("rawMaterial");

                entity.Property(e => e.DrumLotNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("drumLotNumber")
                    .IsFixedLength();

                entity.Property(e => e.ContainerNumber)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("containerNumber")
                    .IsFixedLength();

                entity.Property(e => e.DateUsed)
                    .HasColumnType("date")
                    .HasColumnName("dateUsed");

                entity.Property(e => e.DrumWeight).HasColumnName("drumWeight");

                entity.Property(e => e.MaterialNumber).HasColumnName("materialNumber");

                entity.Property(e => e.ProcessOrder)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("processOrder");

                entity.Property(e => e.SampleSubmitNumber)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("sampleSubmitNumber")
                    .IsFixedLength();

                entity.Property(e => e.SapBatchNumber).HasColumnName("sapBatchNumber");

                entity.Property(e => e.VendorBatchId).HasColumnName("vendorBatchId");

                entity.Property(e => e.VendorId).HasColumnName("vendorId");

                entity.HasOne(d => d.MaterialNumberNavigation)
                    .WithMany(p => p.RawMaterials)
                    .HasForeignKey(d => d.MaterialNumber)
                    .HasConstraintName("FK__rawMateri__mater__35BCFE0A");

                entity.HasOne(d => d.SampleSubmitNumberNavigation)
                    .WithMany(p => p.RawMaterials)
                    .HasForeignKey(d => d.SampleSubmitNumber)
                    .HasConstraintName("FK__rawMateri__sampl__36B12243");

                entity.HasOne(d => d.VendorBatch)
                    .WithMany(p => p.RawMaterials)
                    .HasForeignKey(d => d.VendorBatchId)
                    .HasConstraintName("FK__rawMateri__vendo__38996AB5");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.RawMaterials)
                    .HasForeignKey(d => d.VendorId)
                    .HasConstraintName("FK__rawMateri__vendo__37A5467C");
            });

            modelBuilder.Entity<Receiver>(entity =>
            {
                entity.ToTable("receiver");

                entity.Property(e => e.ReceiverId)
                    .ValueGeneratedNever()
                    .HasColumnName("receiverId");

                entity.Property(e => e.ReceiverName)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("receiverName")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.ToTable("vendor");

                entity.Property(e => e.VendorId).HasColumnName("vendorId");

                entity.Property(e => e.VendorName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("vendorName");
            });

            modelBuilder.Entity<VendorBatchInformation>(entity =>
            {
                entity.HasKey(e => e.BatchId)
                    .HasName("PK__vendorBa__78CCD773230650CE");

                entity.ToTable("vendorBatchInformation");

                entity.Property(e => e.BatchId).HasColumnName("batchId");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.VendorBatchNumber)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("vendorBatchNumber");

                entity.Property(e => e.VendorId).HasColumnName("vendorId");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.VendorBatchInformations)
                    .HasForeignKey(d => d.VendorId)
                    .HasConstraintName("FK__vendorBat__vendo__267ABA7A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
