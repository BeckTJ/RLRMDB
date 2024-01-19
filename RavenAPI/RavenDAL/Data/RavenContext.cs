using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RavenDB.Models;

namespace RavenDB.Data;

public partial class RavenContext : DbContext
{
    public RavenContext()
    {
    }

    public RavenContext(DbContextOptions<RavenContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AlphabeticDate> AlphabeticDates { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<MaterialVendor> MaterialVendors { get; set; }

    public virtual DbSet<PreStartCheck> PreStartChecks { get; set; }

    public virtual DbSet<ProductLevel> ProductLevels { get; set; }

    public virtual DbSet<ProductRun> ProductRuns { get; set; }

    public virtual DbSet<Production> Productions { get; set; }

    public virtual DbSet<RawMaterial> RawMaterials { get; set; }

    public virtual DbSet<SampleRequired> SampleRequireds { get; set; }

    public virtual DbSet<SampleSubmit> SampleSubmits { get; set; }

    public virtual DbSet<SystemStatus> SystemStatuses { get; set; }

    public virtual DbSet<UnitOfIssue> UnitOfIssues { get; set; }

    public virtual DbSet<VendorLot> VendorLots { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=Raven;Persist Security Info=True; Trust Server Certificate=True;User ID=SA; Password = FR*@ger12");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AlphabeticDate>(entity =>
        {
            entity.HasKey(e => e.MonthNumber).HasName("PK__Alphabet__C6DA02F1E99A2577");

            entity.ToTable("AlphabeticDate", "Distillation");

            entity.Property(e => e.MonthNumber).ValueGeneratedNever();
            entity.Property(e => e.AlphabeticCode)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04F119A977FB8");

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

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.MaterialNumber).HasName("PK__Material__E4D2E4BF11CAD88F");

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
            entity.HasKey(e => e.MaterialNumber).HasName("PK__Material__E4D2E4BF628757E8");

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
            entity.Property(e => e.BatchManaged).HasDefaultValueSql("((0))");
            entity.Property(e => e.ProcessOrderRequired).HasDefaultValueSql("((0))");


            entity.HasOne(d => d.ParentMaterialNumberNavigation).WithMany(p => p.MaterialVendors)
                .HasForeignKey(d => d.ParentMaterialNumber)
                .HasConstraintName("FK__MaterialV__Paren__33D4B598");
        });

        modelBuilder.Entity<PreStartCheck>(entity =>
        {
            entity.HasKey(e => e.CheckId).HasName("PK__PreStart__8681570671B04D19");

            entity.ToTable("PreStartChecks", "Distillation");

            entity.Property(e => e.CheckId).HasColumnName("CheckID");
            entity.Property(e => e.HeliumCylinderPsi).HasColumnName("HeliumCylinderPSI");
            entity.Property(e => e.HeliumFlowPsi).HasColumnName("HeliumFlowPSI");

            entity.HasOne(d => d.MaterialNumberNavigation).WithMany(p => p.PreStartChecks)
                .HasForeignKey(d => d.MaterialNumber)
                .HasConstraintName("FK__PreStartC__Mater__5AEE82B9");

            entity.HasOne(d => d.Run).WithMany(p => p.PreStartChecks)
                .HasForeignKey(d => d.RunId)
                .HasConstraintName("FK__PreStartC__RunId__59FA5E80");
        });

        modelBuilder.Entity<ProductLevel>(entity =>
        {
            entity.HasKey(e => e.LevelId).HasName("PK__ProductL__09F03C26E042FDF4");

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
                .HasConstraintName("FK__ProductLe__Produ__5535A963");

            entity.HasOne(d => d.RunNumberNavigation).WithMany(p => p.ProductLevels)
                .HasForeignKey(d => d.RunNumber)
                .HasConstraintName("FK__ProductLe__RunNu__5629CD9C");
        });

        modelBuilder.Entity<ProductRun>(entity =>
        {
            entity.HasKey(e => e.RunId).HasName("PK__ProductR__A259D4DD90B3AD4D");

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
                .HasConstraintName("FK__ProductRu__DrumL__5070F446");

            entity.HasOne(d => d.Employee).WithMany(p => p.ProductRuns)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__ProductRu__Emplo__52593CB8");

            entity.HasOne(d => d.ProductLotNumberNavigation).WithMany(p => p.ProductRuns)
                .HasForeignKey(d => d.ProductLotNumber)
                .HasConstraintName("FK__ProductRu__Produ__5165187F");
        });

        modelBuilder.Entity<Production>(entity =>
        {
            entity.HasKey(e => e.ProductLotNumber).HasName("PK__Producti__36A9702297F2A478");

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

            entity.HasOne(d => d.MaterialNumberNavigation).WithMany(p => p.Productions)
                .HasForeignKey(d => d.MaterialNumber)
                .HasConstraintName("FK__Productio__Mater__4CA06362");

            entity.HasOne(d => d.Sample).WithMany(p => p.Productions)
                .HasForeignKey(d => d.SampleId)
                .HasConstraintName("FK__Productio__Sampl__4D94879B");
        });

        modelBuilder.Entity<RawMaterial>(entity =>
        {
            entity.HasKey(e => e.DrumLotNumber).HasName("PK__RawMater__64C9F8475C2FDE0B");

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
            entity.Property(e => e.VendorLotNumber)
                .HasMaxLength(25)
                .IsUnicode(false);

            entity.HasOne(d => d.Employee).WithMany(p => p.RawMaterials)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__RawMateri__Emplo__49C3F6B7");

            entity.HasOne(d => d.MaterialNumberNavigation).WithMany(p => p.RawMaterials)
                .HasForeignKey(d => d.MaterialNumber)
                .HasConstraintName("FK__RawMateri__Mater__46E78A0C");

            entity.HasOne(d => d.Sample).WithMany(p => p.RawMaterials)
                .HasForeignKey(d => d.SampleId)
                .HasConstraintName("FK__RawMateri__Sampl__47DBAE45");

            entity.HasOne(d => d.VendorLotNumberNavigation).WithMany(p => p.RawMaterials)
                .HasForeignKey(d => d.VendorLotNumber)
                .HasConstraintName("FK__RawMateri__Vendo__48CFD27E");
        });

        modelBuilder.Entity<SampleRequired>(entity =>
        {
            entity.HasKey(e => new { e.MaterialNumber, e.Vln }).HasName("PK__SampleRe__088F292E27EC7D49");

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
            entity.HasKey(e => e.SampleId).HasName("PK__SampleSu__8B99EC6ACC32FAD0");

            entity.ToTable("SampleSubmit", "QualityControl", tb => tb.HasTrigger("SetSampleDates"));

            entity.Property(e => e.EmployeeId)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ExperiationDate).HasColumnType("date");
            entity.Property(e => e.ReviewDate).HasColumnType("date");
            entity.Property(e => e.SampleDate).HasColumnType("date");
            entity.Property(e => e.SampleType)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Employee).WithMany(p => p.SampleSubmits)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__SampleSub__Emplo__30F848ED");
        });

        modelBuilder.Entity<SystemStatus>(entity =>
        {
            entity.HasKey(e => e.StatusCode).HasName("PK__SystemSt__6A7B44FDE5ACE861");

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
            entity.HasKey(e => e.UnitOfIssue1).HasName("PK__UnitOfIs__B79EB8A20FBE0296");

            entity.ToTable("UnitOfIssue", "Materials");

            entity.Property(e => e.UnitOfIssue1)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("UnitOfIssue");
            entity.Property(e => e.Nomenclature)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VendorLot>(entity =>
        {
            entity.HasKey(e => e.VendorLotNumber).HasName("PK__VendorLo__050888CCA6085EB6");

            entity.ToTable("VendorLot", "Materials");

            entity.Property(e => e.VendorLotNumber)
                .HasMaxLength(25)
                .IsUnicode(false);

            entity.HasOne(d => d.MaterialNumberNavigation).WithMany(p => p.VendorLots)
                .HasForeignKey(d => d.MaterialNumber)
                .HasConstraintName("FK__VendorLot__Mater__440B1D61");

            entity.HasOne(d => d.Sample).WithMany(p => p.VendorLots)
                .HasForeignKey(d => d.SampleId)
                .HasConstraintName("FK__VendorLot__Sampl__4316F928");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
