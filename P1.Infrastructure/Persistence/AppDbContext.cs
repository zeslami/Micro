using Microsoft.EntityFrameworkCore;
using P1.Domain.Entities;

namespace P1.Infrastructure.Persistence;

/// <summary>
/// Scaffolded via Scaffold-DbContext (DB-First). The hardcoded OnConfiguring/connection-string
/// that Scaffold normally generates has been REMOVED on purpose: the connection string now comes
/// from appsettings.json through dependency injection (see Infrastructure/DependencyInjection.cs
/// and the AddDbContext call there). Do not add OnConfiguring back.
/// </summary>
public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<ProductGroup> ProductGroups { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductWithGroupView> ProductsWithGroupView { get; set; }

    public virtual DbSet<ProductByGroupResult> ProductByGroupResults { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Username).HasMaxLength(50).IsRequired();
            entity.Property(e => e.PasswordHash).HasMaxLength(256).IsRequired();
            entity.Property(e => e.Email).HasMaxLength(150).IsRequired();
            entity.Property(e => e.Role).HasMaxLength(30).IsRequired();
            entity.HasIndex(e => e.Username).IsUnique();
        });

        modelBuilder.Entity<ProductGroup>(entity =>
        {
            entity.ToTable("ProductGroup");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Description).HasMaxLength(500);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(150).IsRequired();
            entity.Property(e => e.Price).HasColumnType("decimal(18,2)");

            entity.HasOne(d => d.ProductGroup)
                .WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductGroupId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Keyless entity mapped to the SQL View vw_ProductsWithGroup (read-only).
        modelBuilder.Entity<ProductWithGroupView>(entity =>
        {
            entity.HasNoKey();
            entity.ToView("vw_ProductsWithGroup");
        });

        // Keyless entity used only to materialize sp_GetProductsByGroup results; not mapped to a table/view.
        modelBuilder.Entity<ProductByGroupResult>(entity =>
        {
            entity.HasNoKey();
            entity.ToView(null);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
