using Microsoft.EntityFrameworkCore;
using DataAccess.DBContexts.HRMSDB.Models;

namespace DataAccess.DBContexts.HRMSDB
{
    public partial class HRMSDBContext : DbContext
    {
        public HRMSDBContext(DbContextOptions<HRMSDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AuditTrail> AuditTrails { get; set; }
        public virtual DbSet<AuditTrailDetail> AuditTrailDetails { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeRole> EmployeeRoles { get; set; }
        public virtual DbSet<ErrorLog> ErrorLogs { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditTrailDetail>(entity =>
            {
                entity.HasOne(d => d.AuditTrail)
                    .WithMany(p => p.AuditTrailDetails)
                    .HasForeignKey(d => d.AuditTrailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AuditTrailDetail_AuditTrail");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.EmployeeId).ValueGeneratedNever();

                entity.Property(e => e.EmployeeNumber).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<EmployeeRole>(entity =>
            {
                entity.HasOne(d => d.Role)
                    .WithMany(p => p.EmployeeRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Role_EmployeeRole");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasOne(d => d.UserRole)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_UserRole");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
