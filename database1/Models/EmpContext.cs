using Microsoft.EntityFrameworkCore;

namespace database1.Models;

public partial class EmpContext : DbContext
{
    public EmpContext(DbContextOptions<EmpContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Login> Logins { get; set; }
    public virtual DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Login>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("login");

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(250);
            entity.Property(e => e.Username).HasMaxLength(250);

            entity.HasOne(d => d.Role)
                .WithMany(p => p.Logins)
                .HasForeignKey(d => d.Roleid);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("Role");

            entity.Property(e => e.Rname).HasMaxLength(250);
        });
    }
}