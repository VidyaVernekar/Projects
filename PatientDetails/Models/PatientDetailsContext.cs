using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PatientDetails.Models
{
    public partial class PatientDetailsContext : DbContext
    {
        public PatientDetailsContext()
        {
        }

        public PatientDetailsContext(DbContextOptions<PatientDetailsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Drug> Drugs { get; set; } = null!;
        public virtual DbSet<Login> Logins { get; set; } = null!;
        public virtual DbSet<PatientInfo> PatientInfos { get; set; } = null!;
        public virtual DbSet<PatientStatus> PatientStatuses { get; set; } = null!;
        public virtual DbSet<Prescription> Prescriptions { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=PatientDetails;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Drug>(entity =>
            {
                entity.ToTable("Drug");

                entity.Property(e => e.DrugName)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Login__1788CC4C36165D6A");

                entity.ToTable("Login");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Roles");
            });

            modelBuilder.Entity<PatientInfo>(entity =>
            {
                entity.ToTable("PatientInfo");

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("DOB");

                entity.Property(e => e.EmailId)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("EmailID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.PatientInfos)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Status_PatID");
            });

            modelBuilder.Entity<PatientStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                    .HasName("PK__PatientS__C8EE20631B7EB919");

                entity.ToTable("PatientStatus");

                entity.Property(e => e.StatusName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.HasKey(e => e.PreId)
                    .HasName("PK__Prescrip__7024CEC9104B5D1A");

                entity.ToTable("Prescription");

                entity.Property(e => e.PatId).HasColumnName("PatID");

                entity.HasOne(d => d.Drug)
                    .WithMany(p => p.Prescriptions)
                    .HasForeignKey(d => d.DrugId)
                    .HasConstraintName("FK_DrugId");

                entity.HasOne(d => d.Pat)
                    .WithMany(p => p.Prescriptions)
                    .HasForeignKey(d => d.PatId)
                    .HasConstraintName("FK_PatID");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
