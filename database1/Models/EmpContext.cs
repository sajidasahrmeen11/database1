using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace database1.Models;

public partial class EmpContext : DbContext
{
    public EmpContext()
    {
    }

    public EmpContext(DbContextOptions<EmpContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    // ❌ OnConfiguring REMOVE kar diya (IMPORTANT)

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Login>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__login__3214EC07CE89C6DF");

            entity.ToTable("login");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");

            entity.Property(e => e.Password)
                .HasMaxLength(250)
                .HasColumnName("password");

            entity.Property(e => e.Roleid)
                .HasColumnName("roleid");

            entity.Property(e => e.Username)
                .HasMaxLength(250)
                .HasColumnName("username");

            entity.HasOne(d => d.Role)
                .WithMany(p => p.Logins)
                .HasForeignKey(d => d.Roleid)
                .HasConstraintName("FK_login_ToTable");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC07AF0DBE76");

            entity.ToTable("Role");

            entity.Property(e => e.Rname)
                .HasMaxLength(250)
                .HasColumnName("rname");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}