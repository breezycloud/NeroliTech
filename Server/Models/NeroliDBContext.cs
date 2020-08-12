using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NeroliTech.Server.Models
{
    public partial class NeroliDBContext : DbContext
    {
        public NeroliDBContext()
        {
        }

        public NeroliDBContext(DbContextOptions<NeroliDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ExporterDeclaration> ExporterDeclarations { get; set; }
        public virtual DbSet<ExporterShippingDetail> ExporterShippingDetails { get; set; }
        public virtual DbSet<PreshipmentInspectionFinding> PreshipmentInspectionFindings { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=NeroliDatabase");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExporterDeclaration>(entity =>
            {
                entity.ToTable("ExporterDeclaration");

                entity.HasIndex(e => e.CciNo)
                    .HasName("IX_ExporterDeclaration")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CciNo)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.ExporterFobInvoiceValue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.FreightCharges).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.InsuranceCharges).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalInvoiceValue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<ExporterShippingDetail>(entity =>
            {
                entity.HasIndex(e => e.CciNo)
                    .HasName("IX_ExporterShippingDetails_CciNoId");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CciNo)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.HasOne(d => d.CciNoNavigation)
                    .WithMany(p => p.ExporterShippingDetails)
                    .HasPrincipalKey(p => p.CciNo)
                    .HasForeignKey(d => d.CciNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExporterShippingDetails_ExporterDeclaration");
            });

            modelBuilder.Entity<PreshipmentInspectionFinding>(entity =>
            {
                entity.HasIndex(e => e.CciNo)
                    .HasName("IX_PreshipmentInspectionFindings_CciNoId");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ActualNessCharges).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.BalancePaid).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CciNo)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.ExchangeRate).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ForexProceedDueTransaction).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.NessChargePayable).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalFobGoodsValue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalFreightCharges).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalInsuranceCharge).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.CciNoNavigation)
                    .WithMany(p => p.PreshipmentInspectionFindings)
                    .HasPrincipalKey(p => p.CciNo)
                    .HasForeignKey(d => d.CciNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PreshipmentInspectionFindings_PreshipmentInspectionFindings");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.Role).IsRequired();

                entity.Property(e => e.Username).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
