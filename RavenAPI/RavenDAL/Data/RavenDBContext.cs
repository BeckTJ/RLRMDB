using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RavenDAL.Models;

namespace RavenDAL.Data;

public partial class RavenDbContext : DbContext
{
    public RavenDbContext()
    {
    }

    public RavenDbContext(DbContextOptions<RavenDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AlphabeticDate> AlphabeticDates { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<IndicatorSetPoint> IndicatorSetPoints { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<MaterialVendor> MaterialVendors { get; set; }

    public virtual DbSet<PreStartCheck> PreStartChecks { get; set; }

    public virtual DbSet<ProductLevel> ProductLevels { get; set; }

    public virtual DbSet<ProductRun> ProductRuns { get; set; }

    public virtual DbSet<Production> Productions { get; set; }

    public virtual DbSet<RawMaterial> RawMaterials { get; set; }

    public virtual DbSet<Receiver> Receivers { get; set; }

    public virtual DbSet<SampleRequired> SampleRequireds { get; set; }

    public virtual DbSet<SampleSubmit> SampleSubmits { get; set; }

    public virtual DbSet<SystemIndicator> SystemIndicators { get; set; }

    public virtual DbSet<SystemInformation> SystemInformations { get; set; }

    public virtual DbSet<SystemNomenclature> SystemNomenclatures { get; set; }

    public virtual DbSet<SystemReceiver> SystemReceivers { get; set; }

    public virtual DbSet<SystemStatus> SystemStatuses { get; set; }

    public virtual DbSet<UnitOfIssue> UnitOfIssues { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    public virtual DbSet<VendorLot> VendorLots { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source = localhost; Initial Catalog = RavenDB; Persist Security Info = True; Trust Server Certificate = True; User Id= SA; Password = FR*@ger12;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AlphabeticDate>(entity =>
        {
            entity.HasKey(e => e.MonthNumber).HasName("PK__Alphabet__C6DA02F1512FE7CE");

            entity.ToTable("AlphabeticDate", "Distillation");

            entity.Property(e => e.MonthNumber).ValueGeneratedNever();
            entity.Property(e => e.AlphabeticCode)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04F1186D504E5");

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
            entity.HasKey(e => e.SystemId).HasName("PK__Indicato__9394F68A78F07CD1");

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

            entity.HasOne(d => d.MaterialNumberNavigation).WithMany(p => p.IndicatorSetPoints)
                .HasForeignKey(d => d.MaterialNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Indicator__Mater__3A81B327");

            entity.HasOne(d => d.NomenclatureNavigation).WithMany(p => p.IndicatorSetPoints)
                .HasForeignKey(d => d.Nomenclature)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Indicator__Nomen__3B75D760");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.MaterialNumber).HasName("PK__Material__E4D2E4BFDA277202");

            entity.ToTable("Material", "Materials");

            entity.HasIndex(e => e.MaterialNameAbreviation, "IX_Material_NameAbreviation");

            entity.Property(e => e.MaterialNumber).ValueGeneratedNever();
            entity.Property(e => e.MaterialCode)
                .HasMaxLength(3)
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
            entity.Property(e => e.UnitOfIssue)
                .HasMaxLength(2)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MaterialVendor>(entity =>
        {
            entity.HasKey(e => e.MaterialNumber).HasName("PK__Material__E4D2E4BFA4A41811");

            entity.ToTable("MaterialVendor", "Materials");

            entity.Property(e => e.MaterialNumber).ValueGeneratedNever();
            entity.Property(e => e.MaterialCode)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.UnitOfIssue)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.VendorName)
                .HasMaxLength(25)
                .IsUnicode(false);

            entity.HasOne(d => d.ParentMaterialNumberNavigation).WithMany(p => p.MaterialVendors)
                .HasForeignKey(d => d.ParentMaterialNumber)
                .HasConstraintName("FK__MaterialV__Paren__440B1D61");

            entity.HasOne(d => d.VendorNameNavigation).WithMany(p => p.MaterialVendors)
                .HasForeignKey(d => d.VendorName)
                .HasConstraintName("FK__MaterialV__Vendo__4316F928");
        });

        modelBuilder.Entity<PreStartCheck>(entity =>
        {
            entity.HasKey(e => e.CheckId).HasName("PK__PreStart__868157067E251F5C");

            entity.ToTable("PreStartChecks", "Distillation");

            entity.Property(e => e.CheckId).HasColumnName("CheckID");
            entity.Property(e => e.HeliumCylinderPsi).HasColumnName("HeliumCylinderPSI");
            entity.Property(e => e.HeliumFlowPsi).HasColumnName("HeliumFlowPSI");

            entity.HasOne(d => d.MaterialNumberNavigation).WithMany(p => p.PreStartChecks)
                .HasForeignKey(d => d.MaterialNumber)
                .HasConstraintName("FK__PreStartC__Mater__6C190EBB");

            entity.HasOne(d => d.Run).WithMany(p => p.PreStartChecks)
                .HasForeignKey(d => d.RunId)
                .HasConstraintName("FK__PreStartC__RunId__6B24EA82");
        });

        modelBuilder.Entity<ProductLevel>(entity =>
        {
            entity.HasKey(e => e.LevelId).HasName("PK__ProductL__09F03C2622BC3413");

            entity.ToTable("ProductLevels", "Distillation");

            entity.Property(e => e.ProductLotNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SystemStatus)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.VisualVerification).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.ProductLotNumberNavigation).WithMany(p => p.ProductLevels)
                .HasForeignKey(d => d.ProductLotNumber)
                .HasConstraintName("FK__ProductLe__Produ__66603565");

            entity.HasOne(d => d.RunNumberNavigation).WithMany(p => p.ProductLevels)
                .HasForeignKey(d => d.RunNumber)
                .HasConstraintName("FK__ProductLe__RunNu__6754599E");
        });

        modelBuilder.Entity<ProductRun>(entity =>
        {
            entity.HasKey(e => e.RunId).HasName("PK__ProductR__A259D4DD692F18BF");

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

            entity.HasOne(d => d.DrumLotNumberNavigation).WithMany(p => p.ProductRuns)
                .HasForeignKey(d => d.DrumLotNumber)
                .HasConstraintName("FK__ProductRu__DrumL__619B8048");

            entity.HasOne(d => d.Employee).WithMany(p => p.ProductRuns)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__ProductRu__Emplo__6383C8BA");

            entity.HasOne(d => d.ProductLotNumberNavigation).WithMany(p => p.ProductRuns)
                .HasForeignKey(d => d.ProductLotNumber)
                .HasConstraintName("FK__ProductRu__Produ__628FA481");
        });

        modelBuilder.Entity<Production>(entity =>
        {
            entity.HasKey(e => e.ProductLotNumber).HasName("PK__Producti__36A97022DD2A8B74");

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

            entity.HasOne(d => d.MaterialNumberNavigation).WithMany(p => p.Productions)
                .HasForeignKey(d => d.MaterialNumber)
                .HasConstraintName("FK__Productio__Mater__5CD6CB2B");

            entity.HasOne(d => d.ReceiverNameNavigation).WithMany(p => p.Productions)
                .HasForeignKey(d => d.ReceiverName)
                .HasConstraintName("FK__Productio__Recei__5DCAEF64");

            entity.HasOne(d => d.SampleSubmitNumberNavigation).WithMany(p => p.Productions)
                .HasForeignKey(d => d.SampleSubmitNumber)
                .HasConstraintName("FK__Productio__Sampl__5EBF139D");
        });

        modelBuilder.Entity<RawMaterial>(entity =>
        {
            entity.HasKey(e => e.DrumLotNumber).HasName("PK__RawMater__64C9F847A57C4D94");

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
            entity.Property(e => e.VendorLotNumber)
                .HasMaxLength(25)
                .IsUnicode(false);

            entity.HasOne(d => d.Employee).WithMany(p => p.RawMaterials)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__RawMateri__Emplo__59FA5E80");

            entity.HasOne(d => d.MaterialNumberNavigation).WithMany(p => p.RawMaterials)
                .HasForeignKey(d => d.MaterialNumber)
                .HasConstraintName("FK__RawMateri__Mater__571DF1D5");

            entity.HasOne(d => d.SampleSubmitNumberNavigation).WithMany(p => p.RawMaterials)
                .HasForeignKey(d => d.SampleSubmitNumber)
                .HasConstraintName("FK__RawMateri__Sampl__5812160E");

            entity.HasOne(d => d.VendorLotNumberNavigation).WithMany(p => p.RawMaterials)
                .HasForeignKey(d => d.VendorLotNumber)
                .HasConstraintName("FK__RawMateri__Vendo__59063A47");
        });

        modelBuilder.Entity<Receiver>(entity =>
        {
            entity.HasKey(e => e.ReceiverName).HasName("PK__Receiver__FD2F05648F37A0E6");

            entity.ToTable("Receiver", "Engineering");

            entity.Property(e => e.ReceiverName)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<SampleRequired>(entity =>
        {
            entity.HasKey(e => new { e.MaterialNumber, e.Vln }).HasName("PK__SampleRe__088F292E28167E67");

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
            entity.HasKey(e => e.SampleSubmitNumber).HasName("PK__SampleSu__DFFEFAA068B7377D");

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

            entity.HasOne(d => d.Employee).WithMany(p => p.SampleSubmits)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__SampleSub__Emplo__403A8C7D");
        });

        modelBuilder.Entity<SystemIndicator>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SystemIndicator", "Engineering");

            entity.Property(e => e.IndicatorType)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SystemInformation>(entity =>
        {
            entity.HasKey(e => e.MaterialNumber).HasName("PK__SystemIn__E4D2E4BFBC25E5BC");

            entity.ToTable("SystemInformation", "Engineering");

            entity.Property(e => e.MaterialNumber).ValueGeneratedNever();
            entity.Property(e => e.CarbonDrumInstallDate).HasColumnType("date");
            entity.Property(e => e.CollectRefluxRatio)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.PrefractionRefluxRatio)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.SpecificGravity).HasColumnType("decimal(3, 2)");
            entity.Property(e => e.VacuumTrapInstallDate).HasColumnType("date");
        });

        modelBuilder.Entity<SystemNomenclature>(entity =>
        {
            entity.HasKey(e => e.Nomenclature).HasName("PK__SystemNo__8F9251AF19380C60");

            entity.ToTable("SystemNomenclature", "Engineering");

            entity.Property(e => e.Nomenclature)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SystemReceiver>(entity =>
        {
            entity.HasKey(e => e.ReceiverId).HasName("PK__SystemRe__FEBB5F27366C87FC");

            entity.ToTable("SystemReceivers", "Engineering");

            entity.Property(e => e.ReceiverName)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.MaterialNumberNavigation).WithMany(p => p.SystemReceivers)
                .HasForeignKey(d => d.MaterialNumber)
                .HasConstraintName("FK__SystemRec__Mater__35BCFE0A");

            entity.HasOne(d => d.ReceiverNameNavigation).WithMany(p => p.SystemReceivers)
                .HasForeignKey(d => d.ReceiverName)
                .HasConstraintName("FK__SystemRec__Recei__36B12243");
        });

        modelBuilder.Entity<SystemStatus>(entity =>
        {
            entity.HasKey(e => e.StatusCode).HasName("PK__SystemSt__6A7B44FD1AF09713");

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
            entity.HasKey(e => e.UnitOfIssue1).HasName("PK__UnitOfIs__B79EB8A2EF53902D");

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
            entity.HasKey(e => e.VendorName).HasName("PK__Vendor__7320A356945BA040");

            entity.ToTable("Vendor", "Materials");

            entity.Property(e => e.VendorName)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VendorLot>(entity =>
        {
            entity.HasKey(e => e.VendorLotNumber).HasName("PK__VendorLo__050888CCF7F2419D");

            entity.ToTable("VendorLot", "Materials");

            entity.Property(e => e.VendorLotNumber)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.SampleSubmitNumber)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.MaterialNumberNavigation).WithMany(p => p.VendorLots)
                .HasForeignKey(d => d.MaterialNumber)
                .HasConstraintName("FK__VendorLot__Mater__5441852A");

            entity.HasOne(d => d.SampleSubmitNumberNavigation).WithMany(p => p.VendorLots)
                .HasForeignKey(d => d.SampleSubmitNumber)
                .HasConstraintName("FK__VendorLot__Sampl__534D60F1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
