using System;
using System.Collections.Generic;
using AuthNandAuthZ.DataModels;
using Microsoft.EntityFrameworkCore;

namespace AuthNandAuthZ.Data;

public partial class SecureDbContext : DbContext
{
    public SecureDbContext()
    {
    }

    public SecureDbContext(DbContextOptions<SecureDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<RealUser> RealUsers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\Mssqllocaldb;Initial Catalog=secureDb;Integrated Security=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RealUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RealUser__3214EC070B9BB617");

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Role).HasMaxLength(25);
            entity.Property(e => e.UserName).HasMaxLength(200);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC074E569CC2");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
