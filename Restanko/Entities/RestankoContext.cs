using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Restanko.Entities
{
    public partial class RestankoContext : DbContext
    {
        public RestankoContext()
        {
        }

        public static RestankoContext restankoContext { get; set; } = new RestankoContext();

        public RestankoContext(DbContextOptions<RestankoContext> options)
            : base(options)
        {
            
        }

        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Machine> Machines { get; set; } = null!;
        public virtual DbSet<Machinetype> Machinetypes { get; set; } = null!;
        public virtual DbSet<Mark> Marks { get; set; } = null!;
        public virtual DbSet<Repair> Repairs { get; set; } = null!;
        public virtual DbSet<Repairtype> Repairtypes { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;user=root;database=restanko;password=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"));
                optionsBuilder.UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("country");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Machine>(entity =>
            {
                entity.ToTable("machine");

                entity.HasIndex(e => e.MachineTypeId, "FK_Machine_MachineType");

                entity.HasIndex(e => e.MarkId, "FK_Machine_Mark");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Image).HasMaxLength(255);

                entity.Property(e => e.MachineTypeId).HasColumnName("MachineTypeID");

                entity.Property(e => e.MarkId).HasColumnName("MarkID");

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.HasOne(d => d.MachineType)
                    .WithMany(p => p.Machines)
                    .HasForeignKey(d => d.MachineTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Machine_MachineType");

                entity.HasOne(d => d.Mark)
                    .WithMany(p => p.Machines)
                    .HasForeignKey(d => d.MarkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Machine_Mark");
            });

            modelBuilder.Entity<Machinetype>(entity =>
            {
                entity.ToTable("machinetype");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Mark>(entity =>
            {
                entity.ToTable("mark");

                entity.HasIndex(e => e.CountryId, "FK_Mark_Country");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .IsFixedLength();

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Marks)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Mark_Country");
            });

            modelBuilder.Entity<Repair>(entity =>
            {
                entity.ToTable("repair");

                entity.HasIndex(e => e.MachineId, "FK_Repair_Machine");

                entity.HasIndex(e => e.RepairTypeId, "FK_Repair_RepairType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.Repairs)
                    .HasForeignKey(d => d.MachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Repair_Machine");

                entity.HasOne(d => d.RepairType)
                    .WithMany(p => p.Repairs)
                    .HasForeignKey(d => d.RepairTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Repair_RepairType");
            });

            modelBuilder.Entity<Repairtype>(entity =>
            {
                entity.ToTable("repairtype");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Duration).HasColumnName("duration");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsFixedLength();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.RoleId, "FK_User_Role");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Login).HasMaxLength(150);

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .IsFixedLength();

                entity.Property(e => e.Password).HasMaxLength(150);

                entity.Property(e => e.Patryonomic)
                    .HasMaxLength(150)
                    .IsFixedLength();

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Surname)
                    .HasMaxLength(150)
                    .IsFixedLength();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
